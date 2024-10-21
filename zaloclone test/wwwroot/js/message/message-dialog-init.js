document.addEventListener("DOMContentLoaded", function () {
    const textarea = document.querySelector('.aside-chat-input_text');
    const sendButton = document.querySelector('.aside-chat-bar-btn_send');


    if (textarea && sendButton) {
        // Điều chỉnh chiều cao của textarea dựa trên nội dung
        textarea.addEventListener('input', function () {
            this.style.height = '28px'; // Reset chiều cao để tính toán lại
            this.style.height = Math.min(this.scrollHeight, 140) + 'px'; // Điều chỉnh chiều cao dựa trên nội dung nhưng không vượt quá 140px

            if (this.value.trim().length > 0) {
                sendButton.classList.add('visible'); // Hiển thị nút gửi nếu có nội dung
            } else {
                sendButton.classList.remove('visible'); // Ẩn nút gửi nếu không có nội dung
            }
        });

        // Sự kiện nhấn phím Enter để gửi tin nhắn
        textarea.addEventListener("keydown", function (event) {
            if (event.key === "Enter") {
                if (event.ctrlKey) {
                    // Nếu nhấn Ctrl + Enter, cho phép xuống dòng
                    const start = this.selectionStart;
                    const end = this.selectionEnd;
                    this.value =
                        this.value.substring(0, start) + "\n" + this.value.substring(end);
                    this.selectionStart = this.selectionEnd = start + 1; // Đặt con trỏ sau ký tự xuống dòng
                    this.style.height = Math.min(this.scrollHeight, 140) + "px"; // Điều chỉnh lại chiều cao
                } else {
                    // Nếu chỉ nhấn Enter, gửi tin nhắn
                    event.preventDefault(); // Ngăn không cho xuống dòng
                    sendButton.click(); // Giả lập click nút gửi

                    // Tùy chọn: Xóa nội dung sau khi gửi tin nhắn
                    textarea.value = "";
                    textarea.style.height = "28px"; // Reset chiều cao sau khi gửi
                    sendButton.classList.remove("visible");
                }
            }
        });

        sendButton.addEventListener('click', function () {
            const messageText = textarea.value.trim();

            if (messageText.length > 0) {
                // Tạo và thêm tin nhắn mới vào giao diện
                sendMessage(messageText);

                // Xóa nội dung sau khi gửi tin nhắn
                textarea.value = '';
                textarea.style.height = '28px'; // Reset chiều cao sau khi gửi
                sendButton.classList.remove('visible');
            }
        });

    } else {
        console.error('Textarea hoặc Send button không tìm thấy!');
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const emojiBtn = document.querySelector('.emoji-btn');
    const emojiPicker = document.querySelector('.emoji-picker');
    const emojiList = document.querySelectorAll('.emoji-list span');
    // Hiển thị hoặc ẩn danh sách emoji khi click vào nút emoji
    if (emojiBtn && emojiPicker) {
        emojiBtn.addEventListener('click', function () {
            emojiPicker.style.display = emojiPicker.style.display === 'block' ? 'none' : 'block';
            console.log("clock");
        });

        // Chọn emoji và thêm vào textarea
        emojiList.forEach(function (emoji) {
            emoji.addEventListener('click', function () {
                textarea.value += emoji.textContent; // Thêm emoji vào ô input
                emojiPicker.style.display = 'none';  // Ẩn emoji picker sau khi chọn
                if (textarea.value.trim().length > 0) {
                    sendButton.classList.add('visible'); // Hiển thị nút gửi
                }
                textarea.focus();  // Đưa con trỏ vào ô input
            });
        });
    } else {
        console.error('Emoji button hoặc Emoji picker không tìm thấy!');
    }
});
var lastSender = null;
var lastDateTimeCheckPoint = null;
var currentBlockDateDiv = null;
const MIN_MINUTE_GAP = 15;
const MIN_DAY_GAP = 1;
const MIN_YEAR_GAP = 1;
function checkDateTimeSendMinute(sendDateTime, checkPointDateTime, minMinute) {
    if (checkPointDateTime == null) {
        return true;
    }
    // Convert both dates to milliseconds
    const sendTime = new Date(sendDateTime).getTime();
    const checkPointTime = new Date(checkPointDateTime).getTime();

    // Calculate the difference in milliseconds
    const differenceInMs = Math.abs(sendTime - checkPointTime);

    // Convert milliseconds to minutes
    const differenceInMinutes = differenceInMs / (1000 * 60);

    // Check if the difference is more than 15 minutes
    return differenceInMinutes > minMinute;
}
function checkDateTimeSendDay(sendDateTime, checkPointDateTime, minDay) {
    if (checkPointDateTime == null) {
        return false;
    }
    // Convert both dates to milliseconds
    const sendTime = new Date(sendDateTime).getTime();
    const checkPointTime = new Date(checkPointDateTime).getTime();

    // Calculate the difference in milliseconds
    const differenceInMs = Math.abs(sendTime - checkPointTime);

    // Convert milliseconds to days
    const differenceInDays = differenceInMs / (1000 * 60 * 60 * 24);

    // Check if the difference is more than the specified number of days
    return differenceInDays > minDay;
}

function checkDateTimeSendYear(sendDateTime, checkPointDateTime, minYear) {
    if (checkPointDateTime == null) {
        return false;
    }
    // Convert both dates to milliseconds
    const sendTime = new Date(sendDateTime).getTime();
    const checkPointTime = new Date(checkPointDateTime).getTime();

    // Calculate the difference in milliseconds
    const differenceInMs = Math.abs(sendTime - checkPointTime);

    // Convert milliseconds to years
    const differenceInYears = differenceInMs / (1000 * 60 * 60 * 24 * 365);

    // Check if the difference is more than the specified number of years
    return differenceInYears > minYear;
}

function sendMessage(text) {
    // Tạo cấu trúc HTML cho tin nhắn mới

    const newMessageDiv = document.createElement('div');

    const newMessageContentDiv = document.createElement('div');

    const newChatMessageDiv = document.createElement('div');
    newChatMessageDiv.classList.add('chat-message');

    const newChatTextTimeDiv = document.createElement('div');

    const newTextMessageDiv = document.createElement('div');
    newTextMessageDiv.classList.add('text-msg');

    const newChatTimeSpan = document.createElement('span');
    newChatTimeSpan.classList.add('chat-time');

    const reactionDiv = document.createElement('div');
    reactionDiv.classList.add('flex-align-justify-center', 'chat-react');
    reactionDiv.innerHTML = '<i class="fa-solid fa-heart"></i>';

    newMessageDiv.classList.add('flex', 'chat-item', 'me');
    newMessageContentDiv.classList.add('chat-content', 'me');
    newChatTextTimeDiv.classList.add('chat-text-time', 'me');

    const avatarDiv = document.createElement('div');
    avatarDiv.classList.add('avatar-chat-msg');
    const avatar = document.createElement('img');
    avatar.classList.add('avatar-img', 'avt-their');
    avatarDiv.appendChild(avatar);
    newMessageDiv.appendChild(avatarDiv);
    if (lastSender != 'me') {
        avatar.src = '/img/avt.jpeg';
    }
    newMessageDiv.appendChild(newMessageContentDiv);

    newMessageContentDiv.appendChild(newChatMessageDiv);

    newChatMessageDiv.appendChild(newChatTextTimeDiv);
    newChatMessageDiv.appendChild(reactionDiv);

    newChatTextTimeDiv.appendChild(newTextMessageDiv);
    newChatTextTimeDiv.appendChild(newChatTimeSpan);

    lastSender = 'me';

    newTextMessageDiv.innerText = text;
    newChatTimeSpan.innerText = getCurrentTime();

    var isNewBlockDate = checkDateTimeSendMinute(new Date(), lastDateTimeCheckPoint, MIN_MINUTE_GAP);
    if (currentBlockDateDiv == null || isNewBlockDate) {
        var isDisplayDay = checkDateTimeSendDay(new Date(), lastDateTimeCheckPoint, MIN_DAY_GAP);
        var isDisplayYear = checkDateTimeSendYear(new Date(), lastDateTimeCheckPoint, MIN_YEAR_GAP);
        currentBlockDateDiv = document.createElement('div');
        currentBlockDateDiv.classList.add('block-date');
        currentBlockDateDiv.innerHTML = `
            <div class="flex-align-justify-center time-seen">
                <span class="flex-align-justify-center">
                ${getCurrentTimeFormatted(isNewBlockDate, isDisplayDay, isDisplayYear)}
                </span>
            </div>
        `;
        chatContainer.appendChild(currentBlockDateDiv);
    }
    lastDateTimeCheckPoint = new Date();
    currentBlockDateDiv.appendChild(newMessageDiv);

    // Cuộn xuống cuối cùng để thấy tin nhắn mới
    chatContainer.scrollTop = chatContainer.scrollHeight;
    initChatReact();
}

function addMessages(text, sender = null) {
    // Tạo cấu trúc HTML cho tin nhắn mới
    const newMessageDiv = document.createElement('div');

    const newMessageContentDiv = document.createElement('div');

    const newChatMessageDiv = document.createElement('div');
    newChatMessageDiv.classList.add('chat-message');

    const newChatTextTimeDiv = document.createElement('div');

    const newTextMessageDiv = document.createElement('div');
    newTextMessageDiv.classList.add('text-msg');

    const newChatTimeSpan = document.createElement('span');
    newChatTimeSpan.classList.add('chat-time');

    const reactionDiv = document.createElement('div');
    reactionDiv.classList.add('flex-align-justify-center', 'chat-react');
    reactionDiv.innerHTML = '<i class="fa-solid fa-heart"></i>';

    if (sender == null) {
        newMessageDiv.classList.add('flex', 'chat-item');
        newMessageContentDiv.classList.add('chat-content');
        newChatTextTimeDiv.classList.add('chat-text-time');
    }
    else {
        newMessageDiv.classList.add('flex', 'chat-item', sender);
        newMessageContentDiv.classList.add('chat-content', sender);
        newChatTextTimeDiv.classList.add('chat-text-time', sender);
    }        // Thêm lớp tương tự như tin nhắn của mình
    const avatarDiv = document.createElement('div');
    avatarDiv.classList.add('avatar-chat-msg');
    const avatar = document.createElement('img');
    avatar.classList.add('avatar-img', 'avt-their');
    avatarDiv.appendChild(avatar);
    newMessageDiv.appendChild(avatarDiv);
    if (lastSender != sender) {
        avatar.src = '/img/avt.jpeg';
    }
    newMessageDiv.appendChild(newMessageContentDiv);

    newMessageContentDiv.appendChild(newChatMessageDiv);

    newChatMessageDiv.appendChild(newChatTextTimeDiv);
    newChatMessageDiv.appendChild(reactionDiv);

    newChatTextTimeDiv.appendChild(newTextMessageDiv);
    newChatTextTimeDiv.appendChild(newChatTimeSpan);

    lastSender = sender;

    newTextMessageDiv.innerText = text;
    newChatTimeSpan.innerText = getCurrentTime();

    // Thêm tin nhắn mới vào cuối danh sách chat
    chatContainer.prepend(newMessageDiv);

    // Cuộn xuống cuối cùng để thấy tin nhắn mới
    chatContainer.scrollTop = chatContainer.scrollHeight;
    initChatReact();
}

function initChatReact() {
    const reactButtons = document.querySelectorAll('.chat-react');

    if (reactButtons.length > 0) {
        reactButtons.forEach(function (reactButton) {
            reactButton.addEventListener('click', function () {
                this.classList.toggle('active');
                this.classList.toggle('clicked');
            });
        });
    } else {
        console.error('No react buttons found!');
    }
}

function getCurrentTime() {
    const now = new Date();
    return now.getHours() + ":" + (now.getMinutes() < 10 ? '0' : '') + now.getMinutes();
}
function getCurrentTimeFormatted(houraAndMinute = true, dayAndMonth = true, year = true) {
    const now = new Date();
    var result = [];
    if (houraAndMinute) {
        const hours = String(now.getHours()).padStart(2, '0');
        const minutes = String(now.getMinutes()).padStart(2, '0');
        result.push(hours);
        result.push(":");
        result.push(minutes);
    }

    if (dayAndMonth) {
        const day = String(now.getDate()).padStart(2, '0');
        const month = String(now.getMonth() + 1).padStart(2, '0'); // Months are zero-based
        result.push(" ");
        result.push(day);
        result.push("/");
        result.push(month);
    }
    if (year) {
        const year = now.getFullYear();
        result.push("/")
        result.push(year);
    }
    // Format the date and time
    return result.join('');
}