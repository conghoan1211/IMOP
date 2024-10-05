// search-bar-init.js
function initSearchBar() {
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

    lastMsgCount.forEach(function(element) {
        if (element.textContent.trim() === "") {
            element.style.display = 'none';  // Ẩn nếu không có nội dung
        } else {
            element.style.display = 'inline-flex'; // Hiển thị khi có nội dung
        }
    });
    
}

// Export the function if using modules, or attach it to the global object if not
if (typeof module !== 'undefined' && module.exports) {
    module.exports = initSearchBar;
} else {
    window.initSearchBar = initSearchBar;
}
