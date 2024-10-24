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
                addMessage(messageText, 'me');

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
function selectConversation(linkElement) {
    // Remove the "selected" class from all items
    const allItems = document.querySelectorAll('.list-msg .item-msg');
    allItems.forEach(item => item.classList.remove('selected'));

    // Add the "selected" class to the clicked item
    const clickedItem = linkElement.querySelector('.item-msg');
    if (clickedItem) {
        clickedItem.classList.add('selected');
    }
}
function addMessage(text, sender = null) {
    // Create the new message structure
    const newMessageDiv = createElementWithClasses('div', ['flex', 'chat-item']);
    const newMessageContentDiv = createElementWithClasses('div', ['chat-content']);
    const newChatTextTimeDiv = createElementWithClasses('div', ['chat-text-time']);
    if (sender == 'me') {
        newMessageDiv.classList.add('me');
        newMessageContentDiv.classList.add('me');
        newChatTextTimeDiv.classList.add('me');
    }
    const newChatMessageDiv = createElementWithClasses('div', ['chat-message']);
    const newTextMessageDiv = createElementWithClasses('div', ['text-msg']);
    const newChatTimeSpan = createElementWithClasses('span', ['chat-time']);
    const reactionDiv = createElementWithClasses('div', ['flex-align-justify-center', 'chat-react'], '<i class="fa-solid fa-heart"></i>');

    // Add content to the new elements
    newTextMessageDiv.innerText = text;
    newChatTimeSpan.innerText = getCurrentTime();

    // Handle avatar logic
    if (shouldShowAvatar) {
        const avatarDiv = createElementWithClasses('div', ['avatar-chat-msg']);
        const avatar = createElementWithClasses('img', ['avatar-img', 'avt-their']);
        avatar.src = '/img/avt.jpeg';
        avatarDiv.appendChild(avatar);
        newMessageDiv.appendChild(avatarDiv);
    }

    // Build the message structure
    newMessageDiv.appendChild(newMessageContentDiv);
    newMessageContentDiv.appendChild(newChatMessageDiv);
    newChatMessageDiv.appendChild(newChatTextTimeDiv);
    newChatMessageDiv.appendChild(reactionDiv);
    newChatTextTimeDiv.appendChild(newTextMessageDiv);
    newChatTextTimeDiv.appendChild(newChatTimeSpan);

    lastSender = sender;

    const isDisplayHour = checkDateTimeSendMinute(new Date(), lastDateTimeCheckPoint, MIN_MINUTE_GAP);
    // Handle block date creation
    if (!currentBlockDateDiv || isDisplayHour) {

        const isDisplayDay = checkDateTimeSendDay(new Date(), lastDateTimeCheckPoint, MIN_DAY_GAP);
        const isDisplayYear = checkDateTimeSendYear(new Date(), lastDateTimeCheckPoint, MIN_YEAR_GAP);

        currentBlockDateDiv = createElementWithClasses('div', ['block-date'], `
            <div class="flex-align-justify-center time-seen">
                <span class="flex-align-justify-center">
                    ${getCurrentTimeFormatted(isDisplayHour, isDisplayDay, isDisplayYear)}
                </span>
            </div>
        `);
        chatContainer.appendChild(currentBlockDateDiv);
    }

    // Update checkpoint and append the new message
    lastDateTimeCheckPoint = new Date();
    currentBlockDateDiv.appendChild(newMessageDiv);

    // Scroll to the bottom
    chatContainer.scrollTop = chatContainer.scrollHeight;
    initChatReact();
}
function shouldShowAvatar(sender) {
    return lastSender !== sender || !currentBlockDateDiv || checkDateTimeSendMinute(new Date(), lastDateTimeCheckPoint, MIN_MINUTE_GAP);
}
// Helper function to create an element with specified classes and innerHTML (optional)
function createElementWithClasses(tagName, classList, innerHTML = '') {
    const element = document.createElement(tagName);
    element.classList.add(...classList);
    element.innerHTML = innerHTML;
    return element;
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
    if (houraAndMinute == true) {
        const hours = String(now.getHours()).padStart(2, '0');
        const minutes = String(now.getMinutes()).padStart(2, '0');
        result.push(hours);
        result.push(":");
        result.push(minutes);
    }

    if (dayAndMonth == true) {
        const day = String(now.getDate()).padStart(2, '0');
        const month = String(now.getMonth() + 1).padStart(2, '0'); // Months are zero-based
        result.push(" ");
        result.push(day);
        result.push("/");
        result.push(month);
    }
    if (year == true) {
        const year = now.getFullYear();
        result.push("/")
        result.push(year);
    }

    console.log(houraAndMinute + " " + dayAndMonth + " " + year);
    // Format the date and time
    return result.join('');
}