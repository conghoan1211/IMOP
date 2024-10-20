// Logic zoom ảnh profile
function initProfileImageZoom() {
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
}

// Logic zoom ảnh post
function initPostImageZoom() {
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
}

function initImageScroll() {
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
}

// Gọi cả hai hàm khi trang tải
if (typeof module !== "undefined" && module.exports) {
  module.exports = { initProfileImageZoom, initPostImageZoom, initImageScroll };
} else {
  window.initProfileImageZoom = initProfileImageZoom;
  window.initPostImageZoom = initPostImageZoom;
  window.initImageScroll = initImageScroll;

}
