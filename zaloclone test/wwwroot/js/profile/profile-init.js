document.addEventListener("DOMContentLoaded", function () {
    const viewProfile = document.getElementById("view-profile");
    const editProfile = document.getElementById("edit-profile");
    const editButton = document.querySelector(".profile-edit-btn"); // Button to switch to edit mode
    const cancelButton = document.querySelector(".profile-btn-cancel"); // Cancel button in edit mode
    const returnButton = document.querySelector(".return-btn"); // Return button in edit mode

    const changeAvatarBtn = document.getElementById("changeAvatarBtn");

    const fileInput = document.getElementById("fileInput");
    const profileAvatar = document.getElementById("profileAvatar");
    const avatarForm = document.getElementById("avatarForm");

    const openProfileButtons = document.querySelectorAll(".openProfileDialog");
    const dialog = document.getElementById("profileDialog");

    // Function to open the dialog
    function openDialog() {
        dialog.style.display = "flex"; // Show the dialog
        localStorage.setItem("modalProfile", "true"); // Save state to local storage
    }

    // Lặp qua tất cả các nút có class openProfileDialog
    openProfileButtons.forEach((button) => {
        button.addEventListener("click", openDialog); // Open dialog on button click
    });

    const closeButton = document.querySelector("#close-profile-btn");
    if (closeButton) {
        closeButton.addEventListener("click", function () {
            dialog.style.display = "none";
            localStorage.removeItem("modalProfile"); // Remove state from local storage
        });
    } else {
        console.error("Close button not found");
    }

    dialog.addEventListener("click", function (event) {
        if (event.target === this) {
            dialog.style.display = "none"; // Hide dialog
            localStorage.removeItem("modalProfile"); // Remove state from local storage
        }
    });

    // Check local storage on page load
    if (localStorage.getItem("modalProfile") === "true") {
        openDialog(); // Open dialog if the state is true
    }

    // Load the profile view state from local storage
    const profileViewState = localStorage.getItem("profileViewState");
    if (profileViewState === "edit") {
        viewProfile.style.display = "none";
        editProfile.style.display = "block";
    } else {
        viewProfile.style.display = "block";
        editProfile.style.display = "none";
    }

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
            localStorage.setItem("profileViewState", "edit"); // Save edit state
        });

        // Function to switch back to View Profile (on Cancel or Return)
        cancelButton.addEventListener("click", function () {
            editProfile.style.display = "none";
            viewProfile.style.display = "block";
            localStorage.setItem("profileViewState", "view"); // Save view state
        });

        returnButton.addEventListener("click", function () {
            editProfile.style.display = "none";
            viewProfile.style.display = "block";
            localStorage.setItem("profileViewState", "view"); // Save view state
        });
    } else {
        console.error("Profile switch elements not found!");
    }

    // Open file input when the button is clicked
    changeAvatarBtn.addEventListener("click", function () {
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

            // Use FormData to send the file asynchronously
            const formData = new FormData(avatarForm);
            formData.append("Image", file); // Add the file to the FormData object

            // Send the form asynchronously with fetch
            fetch(avatarForm.action, {
                method: 'POST',
                body: formData
            })
                .then(response => {
                    if (response.ok) {
                        return response.text(); // Handle the response (you can update UI or handle success)
                    } else {
                        console.error("Form submission failed");
                    }
                })
                .catch(error => {
                    console.error("Error submitting form:", error);
                });
        }
    });

});

//document.addEventListener("DOMContentLoaded", function () {
//    const usernameInput = document.getElementById('#profile-edit-name'); // Username input field
//    const usernameError = document.createElement('span'); // Create an error message element
//    usernameError.classList.add('register-input-error'); // Add error class for styling
//    usernameInput.parentElement.appendChild(usernameError); // Append error message to the parent of the input

//    // Function to validate username
//    function validateUsername() {
//        const username = usernameInput.value.trim();
//        if (username.length < 5) {
//            usernameError.textContent = 'Tên hiển thị phải ít nhất 5 ký tự.';
//            return false; // Validation failed
//        } else {
//            usernameError.textContent = ''; // Clear error message
//            return true; // Validation passed
//        }
//    }

//    // Validate when user leaves the username input
//    usernameInput.addEventListener('blur', validateUsername);

//    // Optional: Validate on input change for real-time feedback
//    usernameInput.addEventListener('input', validateUsername);

//    // Date input validation
//    const dobInput = document.querySelector('.profile-edit-date');
//    const dobError = document.createElement('span'); // Create an error message element for DOB
//    dobError.classList.add('register-input-error'); // Add error class for styling
//    dobInput.parentElement.appendChild(dobError); // Append error message to the parent of the input

//    // Function to validate DOB
//    function validateDOB() {
//        const dob = dobInput.value;
//        if (!dob) {
//            dobError.textContent = 'Ngày sinh không được để trống.';
//            return false; // Validation failed
//        } else {
//            const selectedDate = new Date(dob);
//            const today = new Date();
//            const minDate = new Date('1890-01-01');
//            if (selectedDate > today) {
//                dobError.textContent = 'Ngày sinh không vượt quá ngày hiện tại!';
//                return false;
//            } else if (selectedDate < minDate) {
//                dobError.textContent = 'Năm sinh phải lớn hơn hoặc bằng 1890!';
//                return false;
//            } else {
//                dobError.textContent = ''; // Clear error message
//                return true; // Validation passed
//            }
//        }
//    }

//    // Validate when user leaves the DOB input
//    dobInput.addEventListener('blur', validateDOB);

//    // Optional: Validate on input change for real-time feedback
//    dobInput.addEventListener('input', validateDOB);
//});

