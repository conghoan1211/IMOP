function initSettingModal() {
    const settingButton = document.querySelector('.nav-btn.nav-setting');
    const settingModal = document.querySelector('.setting-modal');

    // Ensure both elements are available
    if (settingButton && settingModal) {
        settingButton.addEventListener('click', function () {
            settingModal.classList.toggle('active');
        });

        // Optional: Close the modal when clicking outside of it
        document.addEventListener('click', function (event) {
            if (!settingModal.contains(event.target) && !settingButton.contains(event.target)) {
                settingModal.classList.remove('active');
            }
        });
    } else {
        console.error('Setting button or modal not found!');
    }
}

// Export or attach to global object if not using modules
if (typeof module !== 'undefined' && module.exports) {
    module.exports = initSettingModal;
} else {
    window.initSettingModal = initSettingModal;
}

document.addEventListener('DOMContentLoaded', function () {
    initSettingModal();
});
