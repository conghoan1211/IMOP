async function loadHTMLAsync(elementId, filePath) {
    try {
        const response = await fetch(filePath);
        if (!response.ok) throw new Error('Failed to load ' + filePath);
        
        const data = await response.text();
        document.getElementById(elementId).innerHTML = data;
    } catch (error) {
        console.error(error);
    }
}
if (typeof module !== 'undefined' && module.exports) {
    module.exports = loadHTMLAsync;
} else {
    window.loadHTMLAsync = loadHTMLAsync;
}
