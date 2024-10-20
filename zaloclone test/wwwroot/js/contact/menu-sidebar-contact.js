document.addEventListener('DOMContentLoaded', function () {

    // Function to set the active item in local storage
    function setActiveItem(id) {
        localStorage.setItem('activeMenuItem', id);
    }
    // Function to get the active item from local storage
    function getActiveItem() {
        return localStorage.getItem('activeMenuItem');
    }
    // Select all the anchor elements
    const menuItems = document.querySelectorAll('.item-menu');
    // Retrieve and set the active class from local storage on page load
    const activeId = getActiveItem();
    if (activeId) {
        const activeItem = document.getElementById(activeId);
        if (activeItem) {
            activeItem.classList.add('active');
        }
    }
    // Add click event listener to each item
    menuItems.forEach(item => {
        item.addEventListener('click', function () {
            // Remove 'active' class from all items
            menuItems.forEach(i => i.classList.remove('active'));
            // Add 'active' class to the clicked item
            this.classList.add('active');
            // Store the active item in local storage
            setActiveItem(this.id);
        });
    });
}); 