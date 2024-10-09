function initProfileDialog() {
    const viewProfile = document.getElementById('view-profile');
    const editProfile = document.getElementById('edit-profile');
    const editButton = document.querySelector('.profile-edit-btn'); // Button to switch to edit mode
    const cancelButton = document.querySelector('.profile-btn-cancel'); // Cancel button in edit mode
    const returnButton = document.querySelector('.return-btn'); // Return button in edit mode

    const profileChangeAvatarButton = document.querySelector('.profile-change-avatar');
    const fileInput = document.getElementById('fileInput');
    const profileAvatar = document.getElementById('profileAvatar');

    document.querySelector('.avatar.nav-btn').addEventListener('click', function () {
        const dialog = document.getElementById('profileDialog');
        dialog.style.display = 'flex';  // Hiển thị dialog
    });

    const closeButton = document.querySelector('#close-profile-btn');
    if (closeButton) {
        closeButton.addEventListener('click', function () {
            const dialog = document.getElementById('profileDialog');
            dialog.style.display = 'none';
        });
    } else {
        console.error('Close button not found');
    }

    document.querySelector('#profileDialog').addEventListener('click', function (event) {
        if (event.target === this) {
            const dialog = document.getElementById('profileDialog');
            dialog.style.display = 'none';  // Ẩn dialog
        }
    });

    // Ensure all elements are available
    if (viewProfile && editProfile && editButton && cancelButton && returnButton) {
        // Function to switch to Edit Profile
        editButton.addEventListener('click', function () {
            viewProfile.style.display = 'none';
            editProfile.style.display = 'block';
        });

        // Function to switch back to View Profile (on Cancel or Return)
        cancelButton.addEventListener('click', function () {
            editProfile.style.display = 'none';
            viewProfile.style.display = 'block';
            console.log("clicjk cancel");
        });

        returnButton.addEventListener('click', function () {
            editProfile.style.display = 'none';
            viewProfile.style.display = 'block';
        });
    } else {
        console.error('Profile switch elements not found!');
    }

    // Open file input when the button is clicked
    profileChangeAvatarButton.addEventListener('click', function () {
        fileInput.click();
    });

    // Handle file selection
    fileInput.addEventListener('change', function () {
        const file = fileInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (event) {
                profileAvatar.src = event.target.result; // Set the new image source
            };
            reader.readAsDataURL(file); // Read the file as a data URL
        }
    });


    // const settingButton = document.querySelector('.nav-setting');
    // const settingModal = document.querySelector('.setting-modal');

    // // Ensure both elements are available
    // if (settingButton && settingModal) {
    //     settingButton.addEventListener('click', function () {
    //         settingModal.classList.toggle('active');
    //     });

    //     // Optional: Close the modal when clicking outside of it
    //     document.addEventListener('click', function (event) {
    //         if (!settingModal.contains(event.target) && !settingButton.contains(event.target)) {
    //             settingModal.classList.remove('active');
    //         }
    //     });
    // } else {
    //     console.error('Setting button or modal not found!');
    // }

}
// Đảm bảo load dialog đúng cách trước khi hiển thị
// function openProfileDialog(profileData) {
//     console.log("openProfileDialog called"); // Thêm log này để kiểm tra

//     // Kiểm tra xem dialog đã có trong DOM chưa
//     const existingDialog = document.getElementById('profileDialog');

//     // Nếu dialog chưa được tải, gọi hàm load và sau đó hiển thị
//     if (!existingDialog || !existingDialog.innerHTML.trim()) {
//         console.log("Dialog not found or empty, loading dialog...");
//         loadProfileDialog(profileData); // Tải dialog và sau đó hiển thị
//     } else {
//         console.log("Dialog already loaded.");
//         showProfileDialog(profileData); // Hiển thị dialog nếu đã được tải
//     }
// }

// function loadProfileDialog(profileData) {
//     loadHTMLWithCache('profileDialog', 'profile-dialog.html', 'profileDialog', function () {
//         console.log("Dialog loaded into DOM");
//         showProfileDialog(profileData); // Hiển thị dialog sau khi tải xong
//         initProfileDialog(); // Gán sự kiện đóng, chuyển tab, thay avatar...
//     });
// }

// function showProfileDialog(profileData) {
//     const dialog = document.getElementById('profileDialog');
//     if (dialog) {
//         // Cập nhật nội dung của dialog với dữ liệu profileData
//         dialog.querySelector('.profile-avatar').src = profileData.avatarUrl;
//         dialog.querySelector('.profile-name').textContent = profileData.name;

//         // Hiển thị dialog
//         dialog.style.display = 'flex';

//         console.log("Profile dialog opened");

//         // Đảm bảo rằng nút đóng dialog chỉ được gán sự kiện sau khi dialog đã được tải
//         const closeButton = dialog.querySelector('.close-btn');
//         if (closeButton) {
//             closeButton.addEventListener('click', function () {
//                 dialog.style.display = 'none';  // Ẩn dialog
//             });
//         } else {
//             console.error('Close button not found');
//         }

//         // Event để đóng dialog khi nhấn ra ngoài nội dung dialog
//         dialog.addEventListener('click', function (event) {
//             if (event.target === dialog) {
//                 dialog.style.display = 'none';
//             }
//         });
//     } else {
//         console.error("ProfileDialog element not found after loading.");
//     }
// }

// Export or attach to global object if not using modules
if (typeof module !== 'undefined' && module.exports) {
    module.exports = {
        initProfileDialog
        // , openProfileDialog, loadProfileDialog, showProfileDialog
    };
} else {
    window.initProfileDialog = initProfileDialog;
    // window.openProfileDialog = openProfileDialog;
    // window.loadProfileDialog = loadProfileDialog;
    // window.showProfileDialog = showProfileDialog;

}
