// Hàm thêm tin nhắn mới vào danh sách chat
function addMessageToChat(text) {
    // Tạo cấu trúc HTML cho tin nhắn mới
    const newMessage = document.createElement('div');
    newMessage.classList.add('flex', 'chat-item', 'me'); // Thêm lớp tương tự như tin nhắn của mình

    // Tạo nội dung cho tin nhắn
    newMessage.innerHTML = `
        <div class="avatar-chat-msg">
            <img class="avatar-img avt-their" src="/img/avt.jpeg" alt="">
        </div>
        <div class="chat-content me">
            <div class="chat-message">
                <div class="chat-text-time me">
                    <div class="text-msg">${text}</div>
                    <span class="chat-time">${getCurrentTime()}</span>
                </div>
                <div class="flex-align-justify-center chat-react">
                    <i class="fa-solid fa-heart"></i>
                </div>
            </div>
        </div>
    `;

    // Thêm tin nhắn mới vào cuối danh sách chat
    chatContainer.appendChild(newMessage);

    // Cuộn xuống cuối cùng để thấy tin nhắn mới
    chatContainer.scrollTop = chatContainer.scrollHeight;
    initChatReact();
}

// Export the function if using modules, or attach it to the global object if not
if (typeof module !== 'undefined' && module.exports) {
    module.exports = addMessageToChat;

} else {
    window.addMessage = addMessageToChat;
}


