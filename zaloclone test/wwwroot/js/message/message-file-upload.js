// file-upload-init.js
document.addEventListener("DOMContentLoaded", function () {
    const imageButton = document.getElementById('btnSelectImage');
    const fileButton = document.getElementById('btnSelectFile');

    if (imageButton && fileButton) {
        // Xử lý chọn hình ảnh
        imageButton.addEventListener('click', function () {
            const imageInput = document.getElementById('imageInput');
            if (imageInput) {
                imageInput.click(); // Mở cửa sổ chọn tệp hình ảnh
            }
        });

        // Xử lý chọn tệp
        fileButton.addEventListener('click', function () {
            const fileInput = document.getElementById('fileInput');
            if (fileInput) {
                fileInput.click(); // Mở cửa sổ chọn tệp
            }
        });
    } else {
        console.error('Button or file input elements not found!');
    }
});