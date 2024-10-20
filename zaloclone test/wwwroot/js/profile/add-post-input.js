document.addEventListener("DOMContentLoaded", function () {
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
    const modals = document.querySelectorAll(".modalAddPost");
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
            const files = Array.from(event.target.files); // Lấy tất cả các file từ input

            files.forEach(file => {
                if (uploadedImages.length < 5) {
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
        });

    }
});

// Hàm mở modal và cập nhật nội dung
function openEditPost(item, avatar) {
    // Cập nhật các trường trong modal với dữ liệu từ post
    document.getElementById('editPostId').value = item.postId;
    document.getElementById('editPostUsername').textContent = item.username;
    document.getElementById('editPostContent').value = item.content;
    document.getElementById('editPostPrivacy').value = item.privacy;
    document.getElementById('editPostAvatar').src = avatar;

    // Hiển thị modal
    document.getElementById('globalModalEditPost').style.display = 'block';
}

// Đảm bảo đóng modal khi nhấn vào nút close
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.modal-addpost__close').forEach(closeBtn => {
        closeBtn.addEventListener('click', function () {
            this.closest('.modal-addpost').style.display = 'none';
        });
    });
});
