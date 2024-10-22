document.addEventListener("DOMContentLoaded", function () {
    const closeButton = document.querySelector('#close-other-profile');
    if (closeButton) {
        closeButton.addEventListener('click', function () {
            const dialog = document.getElementById('profileOtherDialog');
            dialog.style.display = 'none';
            console.log("click close");
        });
    } else {
        console.error('Close button not found');
    }

    document.querySelector('.avatar-chat-msg').addEventListener('click', function () {
        const dialog = document.getElementById('profileOtherDialog');
        dialog.style.display = 'flex';  // Hiển thị dialog
    });
    document.querySelector('.avt-their').addEventListener('click', function () {
        const dialog = document.getElementById('profileOtherDialog');
        dialog.style.display = 'flex';  // Hiển thị dialog
    });

    document.querySelector('#profileOtherDialog').addEventListener('click', function (event) {
        if (event.target === this) {
            const dialog = document.getElementById('profileOtherDialog');
            dialog.style.display = 'none';  // Ẩn dialog
        }
    });
});