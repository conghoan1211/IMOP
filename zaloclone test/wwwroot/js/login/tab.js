document.addEventListener('DOMContentLoaded', function () {
    const formLogin = document.getElementById('form-login');
    const formForget = document.getElementById('form-forget');
    const forgotPasswordLink = document.querySelector('.login-btn-forgot a');
    const returnButton = document.querySelector('.btn-return');
    
    forgotPasswordLink.addEventListener('click', function (event) {
        event.preventDefault(); // Prevent default behavior
        formLogin.style.display = 'none'; // Hide login form
        formForget.style.display = 'block'; // Show forgot form
    });

    returnButton.addEventListener('click', function () {
        formForget.style.display = 'none'; // Hide forgot form
        formLogin.style.display = 'block'; // Show login form
    });

    const phoneInput = document.querySelector('#form-login input[type="text"]');
    const passwordInput = document.querySelector('#form-login input[type="password"]');
    const loginButton = document.querySelector('.login-btn');

    function checkInputs() {
        if (phoneInput.value.trim() !== '' && passwordInput.value.trim() !== '') {
            loginButton.disabled = false;  // Enable login button
            loginButton.classList.remove('disabled'); // Remove 'disabled' class
        } else {
            loginButton.disabled = true;   // Disable login button
            loginButton.classList.add('disabled');  // Add 'disabled' class
        }
    }

    phoneInput.addEventListener('input', checkInputs);
    passwordInput.addEventListener('input', checkInputs);

    const phoneInputText = document.querySelector('.login-input');

    phoneInputText.addEventListener('input', function() {
        this.value = this.value.replace(/[^0-9]/g, ''); // Remove non-numeric characters
    });
    

    // Disable login button by default
    loginButton.disabled = true;
    loginButton.classList.add('disabled');

    const emailTab = document.querySelector('#email-tab');
    const phoneTab = document.querySelector('#phone-tab');
    const emailInputOTP = document.querySelector('#email-input');
    const phoneInputOTP = document.querySelector('#phone-input');

    // Email tab click event
    emailTab.addEventListener('click', function () {
        emailInputOTP.style.display = 'block';  // Show email input
        phoneInputOTP.style.display = 'none';  // Hide phone input
        emailTab.classList.add('active');      // Activate email tab
        phoneTab.classList.remove('active');   // Deactivate phone tab
        phoneInputField.value = null;
        phoneInputField.value = '';
    });

    // Phone tab click event
    phoneTab.addEventListener('click', function () {
        phoneInputOTP.style.display = 'block';  // Show phone input
        emailInputOTP.style.display = 'none';  // Hide email input
        phoneTab.classList.add('active');      // Activate phone tab
        emailTab.classList.remove('active');   // Deactivate email tab
        emailInputField.value = null; 
        emailInputField.value = ''; 
    });
// Select all input fields with a clear icon
const inputContainers = document.querySelectorAll('.login-info');

// Toggle clear icon visibility based on input
function toggleClearIcon(inputField, clearIcon) {
    if (inputField.value.trim() !== '') {
        clearIcon.style.display = 'flex';  // Show clear icon
    } else {
        clearIcon.style.display = 'none';  // Hide clear icon
    }
}

// Add event listeners for each input-clear icon pair
inputContainers.forEach(function(container) {
    const inputField = container.querySelector('input');
    const clearIcon = container.querySelector('.clear-icon');

    if (inputField && clearIcon) {
        // Toggle the clear icon visibility on input change
        inputField.addEventListener('input', function() {
            toggleClearIcon(inputField, clearIcon);
        });

        // Clear the input field when the clear icon is clicked
        clearIcon.addEventListener('click', function() {
            inputField.value = '';  // Clear input field
            toggleClearIcon(inputField, clearIcon);  // Hide clear icon
        });

        // Initial state for the clear icon
        toggleClearIcon(inputField, clearIcon);
    }
});

});
