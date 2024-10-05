// file-upload-init.js
function initFileUpload() {
    const imageButton = document.getElementById('btnSelectImage');
    const fileButton = document.getElementById('btnSelectFile');

    if (imageButton && fileButton) {
        // Xử lý chọn hình ảnh
        imageButton.addEventListener('click', function() {
            const imageInput = document.getElementById('imageInput');
            if (imageInput) {
                imageInput.click(); // Mở cửa sổ chọn tệp hình ảnh
            }
        });

        // Xử lý chọn tệp
        fileButton.addEventListener('click', function() {
            const fileInput = document.getElementById('fileInput');
            if (fileInput) {
                fileInput.click(); // Mở cửa sổ chọn tệp
            }
        });
    } else {
        console.error('Button or file input elements not found!');
    }
}

// Export the function if using modules, or attach it to the global object if not
if (typeof module !== 'undefined' && module.exports) {
    module.exports = initFileUpload;
} else {
    window.initFileUpload = initFileUpload;
}
