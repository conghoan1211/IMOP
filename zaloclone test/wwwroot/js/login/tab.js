document.addEventListener('DOMContentLoaded', function () {
    const formLogin = document.getElementById('form-login');
    const formForget = document.getElementById('form-forget');
    const forgotPasswordLink = document.querySelector('.login-btn-forgot a');
    const returnButton = document.querySelector('.btn-return');
    
    // Check which form should be displayed from localStorage
    if (localStorage.getItem('formState') === 'forgot') {
        formLogin.style.display = 'none'; // Hide login form
        formForget.style.display = 'block'; // Show forgot form
    } else {
        formLogin.style.display = 'block'; // Show login form
        formForget.style.display = 'none'; // Hide forgot form
    }

    forgotPasswordLink.addEventListener('click', function (event) {
        event.preventDefault(); // Prevent default behavior
        formLogin.style.display = 'none'; // Hide login form
        formForget.style.display = 'block'; // Show forgot form
        localStorage.setItem('formState', 'forgot'); // Save state in localStorage
    });

    returnButton.addEventListener('click', function () {
        formForget.style.display = 'none'; // Hide forgot form
        formLogin.style.display = 'block'; // Show login form
        localStorage.setItem('formState', 'login'); // Save state in localStorage
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

    // Disable login button by default
    loginButton.disabled = true;
    loginButton.classList.add('disabled');

    const emailTab = document.querySelector('#email-tab');
    const phoneTab = document.querySelector('#phone-tab');
    const emailInputOTP = document.querySelector('#email-input');
    const phoneInputOTP = document.querySelector('#phone-input');

    const phoneInputField = document.querySelector('#login-phone-input');
    const emailInputField = document.querySelector('#login-email-input');

    // Email tab click event
    emailTab.addEventListener('click', function () {
        emailInputOTP.style.display = 'flex';  // Show email input
        phoneInputOTP.style.display = 'none';  // Hide phone input
        emailTab.classList.add('active');      // Activate email tab
        phoneTab.classList.remove('active');   // Deactivate phone tab
        phoneInputField.value = '';
        phoneInputField.value = null;
    });

    // Phone tab click event
    phoneTab.addEventListener('click', function () {
        phoneInputOTP.style.display = 'flex';  // Show phone input
        emailInputOTP.style.display = 'none';  // Hide email input
        phoneTab.classList.add('active');      // Activate phone tab
        emailTab.classList.remove('active');   // Deactivate email tab
        emailInputField.value = '';
        emailInputField.value = null; 
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
