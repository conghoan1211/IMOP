// message-list-switcher.js
document.addEventListener("DOMContentLoaded", function () {
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
});

document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.querySelector('.search-input');
    const btnCloseSearch = document.querySelector('.btn-search-close');
    const sidebarMessage = document.querySelectorAll('.message-container')[0]; // Sidebar đầu tiên
    const sidebarSearchFriend = document.querySelectorAll('.message-container')[1]; // Sidebar thứ hai

    // Khi click vào input tìm kiếm
    searchInput.addEventListener('click', function () {
        sidebarMessage.style.display = 'none'; // Ẩn sidebar đầu tiên
        sidebarSearchFriend.style.display = 'block'; // Hiển thị sidebar thứ hai
    });

    // Khi click vào nút đóng
    btnCloseSearch.addEventListener('click', function () {
        sidebarSearchFriend.style.display = 'none'; // Ẩn sidebar thứ hai
        sidebarMessage.style.display = 'block'; // Hiển thị lại sidebar đầu tiên
    });
});


document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.querySelector('.search-input');
    const clearIcon = document.querySelector('.clear-icon');
    const lastMsgCount = document.querySelectorAll('.last-msg-count');

    if (searchInput && clearIcon) {
        // Lắng nghe sự kiện 'input' khi người dùng nhập vào ô tìm kiếm
        searchInput.addEventListener('input', function () {
            if (this.value.trim() !== '') {
                clearIcon.style.display = 'flex'; // Hiện nút clear-icon nếu có nội dung
            } else {
                clearIcon.style.display = 'none'; // Ẩn nút clear-icon nếu không có nội dung
            }
        });

        // Xóa nội dung trong ô tìm kiếm khi click vào nút clear-icon
        clearIcon.addEventListener('click', function () {
            searchInput.value = ''; // Xóa nội dung
            clearIcon.style.display = 'none'; // Ẩn nút clear-icon
            searchInput.focus(); // Đặt lại tiêu điểm vào ô input
        });
    } else {
        console.error('Search input or clear icon element not found!');
    }

    lastMsgCount.forEach(function (element) {
        if (element.textContent.trim() === "") {
            element.style.display = 'none';  // Ẩn nếu không có nội dung
        } else {
            element.style.display = 'inline-flex'; // Hiển thị khi có nội dung
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {

    // Function to set the active item in local storage
    function setActiveItem(id) {
        localStorage.setItem('activeMenuItem', id);
    }
    // Function to get the active item from local storage
    function getActiveItem() {
        return localStorage.getItem('activeMenuItem');
    }
    // Select all the anchor elements
    const menuItems = document.querySelectorAll('.item-menu');
    // Retrieve and set the active class from local storage on page load
    const activeId = getActiveItem();
    if (activeId) {
        const activeItem = document.getElementById(activeId);
        if (activeItem) {
            activeItem.classList.add('active');
        }
    }
    // Add click event listener to each item
    menuItems.forEach(item => {
        item.addEventListener('click', function () {
            // Remove 'active' class from all items
            menuItems.forEach(i => i.classList.remove('active'));
            // Add 'active' class to the clicked item
            this.classList.add('active');
            // Store the active item in local storage
            setActiveItem(this.id);
        });
    });
}); 