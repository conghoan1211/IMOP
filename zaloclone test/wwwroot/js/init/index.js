const htmlCache = {
    header: null,
    sidebar: {},
    aside: {}
};

function loadHTMLWithCache(elementId, filePath, cacheKey, callback) {
    if (htmlCache[cacheKey]) {
        // Nếu nội dung đã có trong cache, chỉ cần gán lại
        document.getElementById(elementId).innerHTML = htmlCache[cacheKey];
        if (callback) callback();
    } else {
        // Nếu chưa có trong cache, tải nội dung và lưu vào cache
        fetch(filePath)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Failed to load ' + filePath);
                }
                return response.text();
            })
            .then(data => {
                document.getElementById(elementId).innerHTML = data;
                htmlCache[cacheKey] = data; // Lưu nội dung vào cache
                if (callback) callback();
            })
            .catch(error => console.error(error));
    }
}

function initHeaderSwitcher() {
    const header = document.getElementById('header');
    header.addEventListener('click', function (event) {
        const target = event.target.closest('.nav-btn');

        if (target) {
            document.querySelectorAll('.nav-btn').forEach(btn => btn.classList.remove('active'));
            target.classList.add('active');

            if (target.querySelector('.fa-comment')) {
                loadHTMLWithCache('sidebar', 'sidebar', 'sidebar-list', function () {
                    initIfExists(initSearchBar);
                    initIfExists(initSettingModal);
                    initIfExists(initMessageListSwitcher);
                });
                loadHTMLWithCache('aside', 'aside', 'asideChat', function () {
                    initIfExists(initSidebarSwitch);
                    initIfExists(initMessageDialog);
                    initIfExists(initFileUpload);
                    initIfExists(addMessageToChat);
                    initIfExists(getCurrentTime);
                    initIfExists(initChatReact);
                    initIfExists(initProfileOtherDialog);
                });
            } else if (target.querySelector('.fa-address-book')) {
                loadHTMLWithCache('sidebar', 'sidebar-contact', 'sidebarContact', function () {
                    initIfExists(initContactMenuSwitcher);
                    initIfExists(initSearchBar);
                });
            } else if (target.querySelector('.fa-square-check')) {
                loadHTMLWithCache('sidebar', 'sidebar-tasks', 'sidebarTasks');
            }
        }
    });
}


// Initialize the sidebar switching logic
function initSidebarSwitch() {
    const searchInput = document.querySelector('.search-input');
    if (searchInput) {
        searchInput.addEventListener('click', function () {
            loadHTMLWithCache('sidebar', 'sidebar-search', 'sidebar-search', function () {
                initIfExists(initSearchBar);
            });
        });
        document.addEventListener('click', function (event) {
            const isCloseBtn = event.target.classList.contains('btn-search-close');
            if (isCloseBtn) {
                loadHTMLWithCache('sidebar', 'sidebar', 'sidebar-list', function () {
                    initIfExists(initSidebarSwitch);
                    initIfExists(initSearchBar);
                    initIfExists(initMessageListSwitcher);
                    initIfExists(initSettingModal);
                });
            }
        });
    } else {
        console.error('Search input not found!');
    }
}

function initContactMenuSwitcher() {
    const friendListBtn = document.getElementById('friendList');
    const groupListBtn = document.getElementById('groupList');
    const friendRequestBtn = document.getElementById('friendRequest');
    const groupInviteBtn = document.getElementById('groupInvite');
    const allButtons = [friendListBtn, groupListBtn, friendRequestBtn, groupInviteBtn]; // Danh sách tất cả các button

    friendListBtn.classList.add('active');
    function removeActiveClass() {
        allButtons.forEach(btn => btn.classList.remove('active'));
    }
    friendListBtn.addEventListener('click', function () {
        removeActiveClass(); // Bỏ chọn các nút khác
        friendListBtn.classList.add('active'); // Đánh dấu nút này là đã chọn
        loadHTMLWithCache('aside', 'aside-contact', 'aside-contact', function () {
            console.log('contact list loaded');
            if (typeof openFriendOptions === 'function') openFriendOptions();
            if (typeof initSearchBar === 'function') initSearchBar();
        });
    });
    groupListBtn.addEventListener('click', function () {
        removeActiveClass();
        groupListBtn.classList.add('active');
        loadHTMLWithCache('aside', 'aside-groups', 'aside-groups', function () {
            console.log('Group list loaded');
            if (typeof initSearchBar === 'function') initSearchBar();
        });
    });
    friendRequestBtn.addEventListener('click', function () {
        removeActiveClass();
        friendRequestBtn.classList.add('active');
        loadHTMLWithCache('aside', 'aside-invitations', 'aside-invitations', function () {
            console.log('Friend request loaded');
        });
    });
    groupInviteBtn.addEventListener('click', function () {
        removeActiveClass();
        groupInviteBtn.classList.add('active');
        loadHTMLWithCache('aside', 'group-invite', 'group-invite', function () {
            console.log('Group invite loaded');
        });
    });
}


document.addEventListener('DOMContentLoaded', function () {
    Promise.allSettled([
        loadHTMLPromise('header', 'header'),
        loadHTMLPromise('sidebar', 'sidebar'),
        loadHTMLPromise('aside', 'aside'),
    ]).then(() => {
        initIfExists(initProfileDialog);
        initIfExists(initSidebarSwitch);
        initIfExists(initHeaderSwitcher);
        initIfExists(initSearchBar);
        initIfExists(initSettingModal);
        initIfExists(initMessageListSwitcher);
        initIfExists(initMessageDialog);
        initIfExists(initFileUpload);
        initIfExists(addMessageToChat);
        initIfExists(getCurrentTime);
        initIfExists(initChatReact);
        initIfExists(initProfileOtherDialog);

    }).catch(error => console.error(error));
});



// document.addEventListener('DOMContentLoaded', async function () {
//     try {
//         // Load each section sequentially
//         await loadHTMLAsync('header', 'header.cshtml');
//         initHeaderSwitcher(); // Initialize after header is loaded

//         await loadHTMLAsync('sidebar', 'sidebar.cshtml');
//         if (typeof initMessageListSwitcher === 'function') initMessageListSwitcher();
//         console.log("init search 1");
//         await loadHTMLAsync('aside', 'aside.cshtml');
//         if (typeof initMessageDialog === 'function') initMessageDialog();
//         if (typeof initFileUpload === 'function') initFileUpload();
//         if (typeof addMessageToChat === 'function') addMessageToChat();
//         if (typeof getCurrentTime === 'function') getCurrentTime();
//         if (typeof initChatReact === 'function') initChatReact();
//     } catch (error) {
//         console.error(error);
//     }
// });