document.addEventListener("DOMContentLoaded", function () {
    const viewProfile = document.getElementById("view-profile");
    const editProfile = document.getElementById("edit-profile");
    const editButton = document.querySelector(".profile-edit-btn"); // Button to switch to edit mode
    const cancelButton = document.querySelector(".profile-btn-cancel"); // Cancel button in edit mode
    const returnButton = document.querySelector(".return-btn"); // Return button in edit mode

    const profileChangeAvatarButton = document.querySelector(
        ".profile-change-avatar"
    );
    const fileInput = document.getElementById("fileInput");
    const profileAvatar = document.getElementById("profileAvatar");

    const openProfileButtons = document.querySelectorAll(".openProfileDialog");
    const dialog = document.getElementById("profileDialog");

    // Lặp qua tất cả các nút có class openProfileDialog
    openProfileButtons.forEach((button) => {
        button.addEventListener("click", function () {
            dialog.style.display = "flex"; // Hiển thị dialog
            console.log("Profile dialog opened");
        });
    });
    const closeButton = document.querySelector("#close-profile-btn");
    if (closeButton) {
        closeButton.addEventListener("click", function () {
            const dialog = document.getElementById("profileDialog");
            dialog.style.display = "none";
        });
    } else {
        console.error("Close button not found");
    }

    document.querySelector("#profileDialog")
        .addEventListener("click", function (event) {
            if (event.target === this) {
                const dialog = document.getElementById("profileDialog");
                dialog.style.display = "none"; // Ẩn dialog
            }
        });

    // Ensure all elements are available
    if (
        viewProfile &&
        editProfile &&
        editButton &&
        cancelButton &&
        returnButton
    ) {
        // Function to switch to Edit Profile
        editButton.addEventListener("click", function () {
            viewProfile.style.display = "none";
            editProfile.style.display = "block";
        });

        // Function to switch back to View Profile (on Cancel or Return)
        cancelButton.addEventListener("click", function () {
            editProfile.style.display = "none";
            viewProfile.style.display = "block";
        });

        returnButton.addEventListener("click", function () {
            editProfile.style.display = "none";
            viewProfile.style.display = "block";
        });
    } else {
        console.error("Profile switch elements not found!");
    }

    // Open file input when the button is clicked
    profileChangeAvatarButton.addEventListener("click", function () {
        fileInput.click();
    });

    // Handle file selection
    fileInput.addEventListener("change", function () {
        const file = fileInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (event) {
                profileAvatar.src = event.target.result; // Set the new image source
            };
            reader.readAsDataURL(file); // Read the file as a data URL
        }
    });
});
