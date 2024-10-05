// Helper function that returns a promise for loadHTML
function loadHTMLPromise(elementId, filePath) {
    return fetch(filePath)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to load ' + filePath);
            }
            return response.text();
        })
        .then(data => {
            document.getElementById(elementId).innerHTML = data;
        })
        .catch(error => console.error(error));
}

if (typeof module !== 'undefined' && module.exports) {
    module.exports = loadHTMLPromise;
} else {
    window.loadHTMLPromise = loadHTMLPromise;
}
