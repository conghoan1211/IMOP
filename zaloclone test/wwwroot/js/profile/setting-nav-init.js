document.addEventListener("DOMContentLoaded", function () {
    const settingButton = document.querySelector(".nav-setting");
    const settingModal = document.querySelector(".setting-modal");
    const changePasswordButton = document.querySelector(
        ".setting-btn:nth-child(2)"
    ); // Lấy nút Đổi mật khẩu
    const changePassDialog = document.getElementById("changePassDialog");
    const closeBtn = changePassDialog.querySelector(".close-btn");
    const cancelBtn = changePassDialog.querySelector(".changepass-cancel");

    // Ensure both elements are available
    if (settingButton && settingModal) {
        settingButton.addEventListener("click", function () {
            settingModal.classList.toggle("active");
        });

        // Optional: Close the modal when clicking outside of it
        document.addEventListener("click", function (event) {
            if (
                !settingModal.contains(event.target) &&
                !settingButton.contains(event.target)
            ) {
                settingModal.classList.remove("active");
            }
        });
    } else {
        console.error("Setting button or modal not found!");
    }

    // Show change password dialog when "Đổi mật khẩu" button is clicked
    if (changePasswordButton && changePassDialog) {
        changePasswordButton.addEventListener("click", function () {
            changePassDialog.style.display = "flex"; // Hiển thị dialog thay đổi mật khẩu
        });

        // Close the change password dialog when close button or cancel button is clicked
        closeBtn.addEventListener("click", function () {
            changePassDialog.style.display = "none"; // Ẩn dialog khi nhấn nút đóng
        });

        cancelBtn.addEventListener("click", function () {
            changePassDialog.style.display = "none"; // Ẩn dialog khi nhấn nút hủy
        });

        // Optional: Close when clicking outside the dialog content
        document.addEventListener("click", function (event) {
            if (
                changePassDialog.style.display === "flex" &&
                !changePassDialog.contains(event.target) &&
                !changePasswordButton.contains(event.target)
            ) {
                changePassDialog.style.display = "none";
            }
        });
    } else {
        console.error("Change password button or dialog not found!");
    }
});
