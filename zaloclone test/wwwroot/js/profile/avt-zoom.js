// Logic zoom ảnh profile
document.addEventListener("DOMContentLoaded", function () {
    const profileImg = document.getElementById("avatar-img"); // Ảnh profile
    const profileModal = document.getElementById("imageModal"); // Modal ảnh profile
    const profileModalImg = document.getElementById("imageModalImg"); // Ảnh trong modal
    const profileCloseBtn = document.querySelector("#imgProfileZoomClose"); // Nút đóng modal profile

    // Khi người dùng nhấp vào ảnh profile
    profileImg.addEventListener("click", function () {
        profileModal.style.display = "flex"; // Hiển thị modal
        profileModalImg.src = this.src; // Hiển thị ảnh trong modal
    });

    // Đóng modal khi nhấp vào nút đóng
    profileCloseBtn.addEventListener("click", function () {
        profileModal.style.display = "none"; // Đóng modal
    });

    // Đóng modal khi click ra ngoài nội dung ảnh
    profileModal.addEventListener("click", function (event) {
        if (event.target === profileModal) {
            profileModal.style.display = "none"; // Đóng modal
        }
    });
});

// Logic zoom ảnh post
document.addEventListener("DOMContentLoaded", function () {

    const postImgs = document.querySelectorAll(".aprofile-post__item-img"); // Ảnh post
    const postModal = document.getElementById("openImgPostZoom"); // Modal ảnh post
    const postModalImg = document.getElementById("imagePostImg"); // Ảnh trong modal post
    const postCloseBtn = document.querySelector("#imagePostZoomClose");

    // Khi người dùng nhấp vào một ảnh post
    postImgs.forEach((img) => {
        img.addEventListener("click", function () {
            postModal.style.display = "flex"; // Hiển thị modal
            postModalImg.src = this.src; // Hiển thị ảnh trong modal
        });
    });

    // Đóng modal khi nhấp vào nút đóng
    postCloseBtn.addEventListener("click", function () {
        postModal.style.display = "none"; // Đóng modal
    });

    // Đóng modal khi click ra ngoài nội dung ảnh
    postModal.addEventListener("click", function (event) {
        if (event.target === postModal) {
            postModal.style.display = "none"; // Đóng modal
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {

    const imageList = document.querySelector(".aprofile-post__list-img");
    let isDown = false;
    let startX;
    let scrollLeft;

    // Khi người dùng nhấn giữ chuột
    imageList.addEventListener("mousedown", (e) => {
        if (e.button !== 0) return; // Chỉ kích hoạt khi nhấn chuột trái (button 0)
        isDown = true;
        imageList.classList.add("active");
        startX = e.pageX - imageList.offsetLeft;
        scrollLeft = imageList.scrollLeft;
        imageList.style.cursor = "grabbing"; // Thay đổi con trỏ khi giữ chuột
    });

    // Khi chuột ra khỏi vùng danh sách ảnh
    imageList.addEventListener("mouseleave", () => {
        isDown = false;
        imageList.style.cursor = "grab"; // Trở lại con trỏ bình thường
    });

    // Khi người dùng nhả chuột
    imageList.addEventListener("mouseup", () => {
        isDown = false;
        imageList.style.cursor = "grab"; // Trở lại con trỏ bình thường
    });

    // Khi người dùng di chuyển chuột (kéo)
    imageList.addEventListener("mousemove", (e) => {
        if (!isDown) return; // Chỉ thực hiện nếu đang giữ chuột
        e.preventDefault();
        const x = e.pageX - imageList.offsetLeft;
        const walk = (x - startX) * 2; // Hệ số nhân để điều chỉnh tốc độ trượt
        imageList.scrollLeft = scrollLeft - walk;
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const timeElements = document.querySelectorAll('.format-time');

    timeElements.forEach(function (element) {
        const postTime = new Date(element.getAttribute('data-time'));
        const currentTime = new Date();
        const timeDiff = Math.abs(currentTime - postTime); // Thời gian chênh lệch tính bằng milliseconds

        const seconds = Math.floor(timeDiff / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);

        let timeAgo = '';
        if (days === 1) {
            timeAgo = '1d';
        } else if (days > 1) {
            timeAgo = days + 'd';
        } else if (hours === 1) {
            timeAgo = hours + 'h';
        } else if (hours > 1) {
            timeAgo = hours + 'h';
        } else if (minutes === 1) {
            timeAgo = 'a minute ago';
        } else if (minutes > 1) {
            timeAgo = minutes + 'm';
        } else if (seconds === 1) {
            timeAgo = seconds + 's';
        } else {
            timeAgo = seconds + ' seconds ago';
        }
        element.textContent = timeAgo; // Cập nhật nội dung của div với khoảng thời gian tính được
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const timeElements = document.querySelectorAll('.format-date');

    timeElements.forEach(function (element) {
        const postTime = new Date(element.getAttribute('data-time'));
        const currentTime = new Date();
        const timeDiff = Math.abs(currentTime - postTime); // Thời gian chênh lệch tính bằng milliseconds

        const seconds = Math.floor(timeDiff / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);

        let timeAgo = '';
        if (days > 0) {
            // Nếu là ngày khác, hiển thị theo định dạng YYYY-MM-DD
            const year = postTime.getFullYear();
            const month = String(postTime.getMonth() + 1).padStart(2, '0');
            const day = String(postTime.getDate()).padStart(2, '0');
            timeAgo = `${year}-${month}-${day}`;
        } else if (hours === 1) {
            timeAgo = '1 hour ago';
        } else if (hours > 1) {
            timeAgo = `${hours} hours ago`;
        } else if (minutes === 1) {
            timeAgo = '1 minute ago';
        } else if (minutes > 1) {
            timeAgo = `${minutes} minutes ago`;
        } else if (seconds <= 1) {
            timeAgo = 'just now';
        } else {
            timeAgo = `${seconds} seconds ago`;
        }

        element.textContent = timeAgo; // Cập nhật nội dung của div với khoảng thời gian tính được
    });
});
