// message-list-switcher.js
function initMessageListSwitcher() {
    const allMessagesBtn = document.querySelector('#btnAll');
    const unreadMessagesBtn = document.querySelector('#btnUnread');
    const allMessagesList = document.querySelector('.list-msg');
    const unreadMessagesList = document.querySelector('.list-msg-2');

    if (allMessagesBtn && unreadMessagesBtn && allMessagesList && unreadMessagesList) {
        // Hiển thị tất cả tin nhắn khi người dùng click vào nút "Tất cả"
        allMessagesBtn.addEventListener('click', function () {
            allMessagesList.style.display = 'block';
            unreadMessagesList.style.display = 'none';

            // Cập nhật trạng thái của các nút
            allMessagesBtn.classList.add('selected');
            unreadMessagesBtn.classList.remove('selected');
        });

        // Hiển thị chỉ tin nhắn chưa đọc khi người dùng click vào nút "Chưa đọc"
        unreadMessagesBtn.addEventListener('click', function () {
            unreadMessagesList.style.display = 'block';
            allMessagesList.style.display = 'none';

            // Cập nhật trạng thái của các nút
            unreadMessagesBtn.classList.add('selected');
            allMessagesBtn.classList.remove('selected');
        });
    } else {
        console.error('Message list elements or buttons not found!');
    }
}

// Export the function if using modules, or attach nó vào global object nếu không
if (typeof module !== 'undefined' && module.exports) {
    module.exports = initMessageListSwitcher;
} else {
    window.initMessageListSwitcher = initMessageListSwitcher;
}
