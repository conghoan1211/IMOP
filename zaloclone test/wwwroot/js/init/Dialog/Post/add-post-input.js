function initAddPost() {
  const textarea = document.querySelector(".modal-addpost__input-text");

  // Kiểm tra nếu textarea tồn tại
  if (textarea) {
    textarea.addEventListener("input", function () {
      this.style.height = "28px";
      this.style.height = Math.min(this.scrollHeight, 170) + "px";
    });
  }

  /**open add post modal */
  const postMoreButtons = document.querySelectorAll(".openAddPost");
  const modals = document.querySelectorAll(".modal-addpost");
  const closeBtn = document.querySelector(".modal-addpost__close");

  postMoreButtons.forEach((button, index) => {
    const modal = modals[index];

    button.addEventListener("click", function (event) {
      event.stopPropagation(); // Ngăn chặn sự kiện click truyền ra ngoài
      modal.style.display = modal.style.display === "none" ? "block" : "none"; // Toggle modal
    });

    closeBtn.addEventListener("click", function () {
      modal.style.display = "none"; // Đóng modal
    });
  });

  /** Image upload and preview (Max 5 images) */
  const uploadImageButton = document.getElementById("upload-image-btn");
  const imageInput = document.getElementById("image-upload-input");
  const imageListContainer = document.querySelector(".modal-addpost__list-img");
  let uploadedImages = [];

  if (uploadImageButton && imageInput && imageListContainer) {
    // Khi click vào icon upload hình ảnh
    uploadImageButton.addEventListener("click", function () {
      if (uploadedImages.length < 5) {
        imageInput.click(); // Mở input file để chọn ảnh
      } else {
        alert("Bạn chỉ có thể thêm tối đa 5 ảnh.");
      }
    });

    // Khi người dùng chọn ảnh
    imageInput.addEventListener("change", function (event) {
      const file = event.target.files[0]; // Lấy file từ input
      if (file && uploadedImages.length < 5) {
        const imageUrl = URL.createObjectURL(file); // Tạo URL tạm thời từ file
        uploadedImages.push(imageUrl); // Lưu trữ URL của ảnh đã upload

        // Tạo thẻ div chứa ảnh và nút xóa
        const imageWrapper = document.createElement("div");
        imageWrapper.classList.add("modal-addpost__item-wrapper");

        // Tạo thẻ img mới để hiển thị ảnh
        const newImage = document.createElement("img");
        newImage.src = imageUrl;
        newImage.classList.add("modal-addpost__item-img");

        const deleteButton = document.createElement("button");
        deleteButton.innerHTML = "&times;"; // Icon nút xóa
        deleteButton.classList.add("modal-addpost__delete-btn");

        deleteButton.addEventListener("click", function () {
          imageWrapper.remove();

          // Xóa ảnh khỏi danh sách uploadedImages
          uploadedImages = uploadedImages.filter((img) => img !== imageUrl);
        });

        // Thêm ảnh và nút xóa vào imageWrapper
        imageWrapper.appendChild(newImage);
        imageWrapper.appendChild(deleteButton);

        // Thêm imageWrapper vào container chứa ảnh
        imageListContainer.appendChild(imageWrapper);
      }

      if (uploadedImages.length === 5) {
        alert("Bạn đã thêm đủ 5 ảnh.");
      }
    });
  }
}

if (typeof module !== "undefined" && module.exports) {
  module.exports = initAddPost;
} else {
  window.initAddPost = initAddPost;
}
