function openFriendOptions(button) {
    document.querySelectorAll('.friend-options').forEach(menu => {
        menu.style.display = 'none';
    });

    const friendOptions = button.parentElement.querySelector('.friend-options');
    if (friendOptions) {
        friendOptions.style.display = 'block';
    }


    document.addEventListener('click', function(event) {
        if (!button.contains(event.target) && 
        !friendOptions.contains(event.target) &&
        event.target.tagName !== 'LI') {
        friendOptions.style.display = 'none';
    }
    });
}

function viewInfo(name) {
    alert('Xem thông tin của ' + name);
}

function blockFriend(name) {
    alert('Chặn ' + name);
}

function deleteFriend(name) {
    alert('Xóa ' + name + ' khỏi danh sách bạn bè');
}


// Export the function if using modules, or attach it to the global object if not
if (typeof module !== 'undefined' && module.exports) {
    module.exports = openFriendOptions;
    module.exports = viewInfo;
    module.exports = blockFriend;
    module.exports = deleteFriend;
} else {
    window.openFriendOptions = openFriendOptions;
    window.viewInfo = viewInfo;
    window.blockFriend = blockFriend;
    window.deleteFriend = deleteFriend;

}