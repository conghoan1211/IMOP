// chat-react-init.js
function initChatReact() {
    const reactButtons = document.querySelectorAll('.chat-react');

    if (reactButtons.length > 0) {
        reactButtons.forEach(function(reactButton) {
            reactButton.addEventListener('click', function() {
                this.classList.toggle('active');
                this.classList.toggle('clicked');
            });
        });
    } else {
        console.error('No react buttons found!');
    }
}

// Export or attach to global object if not using modules
if (typeof module !== 'undefined' && module.exports) {
    module.exports = initChatReact;
} else {
    window.initChatReact = initChatReact;
}
