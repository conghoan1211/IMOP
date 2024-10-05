// Hàm lấy giờ hiện tại
function getCurrentTime() {
    const now = new Date();
    return now.getHours() + ":" + (now.getMinutes() < 10 ? '0' : '') + now.getMinutes();
}

function initIfExists(initFunction) {
    if (typeof initFunction === 'function') initFunction();
}


// Export the function if using modules, or attach it to the global object if not
if (typeof module !== 'undefined' && module.exports) {
    module.exports = getCurrentTime;
    module.exports = initIfExists;


} else {
    window.getCurrentTime = getCurrentTime;
    window.initIfExists = initIfExists;

}

