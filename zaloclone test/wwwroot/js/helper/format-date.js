document.addEventListener('DOMContentLoaded', function () {
    const timeElements = document.querySelectorAll('.format-time');

    timeElements.forEach(function (element) {
        const postTime = new Date(element.getAttribute('data-time'));
        const currentTime = new Date();
        const timeDiff = Math.abs(currentTime - postTime); // Thời gian chênh lệch tính bằng milliseconds

        const seconds = Math.floor(timeDiff / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);

        let timeAgo = '';
        if (days === 1) {
            timeAgo = '1d';
        } else if (days > 1) {
            timeAgo = days + 'd';
        } else if (hours === 1) {
            timeAgo = hours + 'h';
        } else if (hours > 1) {
            timeAgo = hours + 'h';
        } else if (minutes === 1) {
            timeAgo = 'a minute ago';
        } else if (minutes > 1) {
            timeAgo = minutes + 'm';
        } else if (seconds === 1) {
            timeAgo = seconds + 's';
        } else {
            timeAgo = seconds + ' seconds ago';
        }
        element.textContent = timeAgo; // Cập nhật nội dung của div với khoảng thời gian tính được
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const timeElements = document.querySelectorAll('.format-date');

    timeElements.forEach(function (element) {
        const postTime = new Date(element.getAttribute('data-time'));
        const currentTime = new Date();
        const timeDiff = Math.abs(currentTime - postTime); // Thời gian chênh lệch tính bằng milliseconds

        const seconds = Math.floor(timeDiff / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);

        let timeAgo = '';
        if (days > 0) {
            // Nếu là ngày khác, hiển thị theo định dạng YYYY-MM-DD
            const year = postTime.getFullYear();
            const month = String(postTime.getMonth() + 1).padStart(2, '0');
            const day = String(postTime.getDate()).padStart(2, '0');
            timeAgo = `${year}-${month}-${day}`;
        } else if (hours === 1) {
            timeAgo = '1 hour ago';
        } else if (hours > 1) {
            timeAgo = `${hours} hours ago`;
        } else if (minutes === 1) {
            timeAgo = '1 minute ago';
        } else if (minutes > 1) {
            timeAgo = `${minutes} minutes ago`;
        } else if (seconds <= 1) {
            timeAgo = 'just now';
        } else {
            timeAgo = `${seconds} seconds ago`;
        }

        element.textContent = timeAgo; // Cập nhật nội dung của div với khoảng thời gian tính được
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const timeElements = document.querySelectorAll('.format-dateonly');

    timeElements.forEach(function (element) {
        const postTime = new Date(element.getAttribute('data-time'));
        const currentTime = new Date();
        const timeDiff = Math.abs(currentTime - postTime); // Thời gian chênh lệch tính bằng milliseconds

        const seconds = Math.floor(timeDiff / 1000);
        const minutes = Math.floor(seconds / 60);
        const hours = Math.floor(minutes / 60);
        const days = Math.floor(hours / 24);
        const years = Math.floor(days / 365);

        let timeAgo = '';
        if (years > 0) {
            // Nếu là hơn 1 năm, hiển thị theo định dạng YYYY-MM-DD
            const year = postTime.getFullYear();
            const month = String(postTime.getMonth() + 1).padStart(2, '0');
            const day = String(postTime.getDate()).padStart(2, '0');
            timeAgo = `${day}/${month}/${year}`;
        } else if (days > 0) {
            // Nếu dưới 1 năm, hiển thị chỉ ngày và tháng
            const month = String(postTime.getMonth() + 1).padStart(2, '0');
            const day = String(postTime.getDate()).padStart(2, '0');
            timeAgo = `${day}/${month}`;
        } else if (hours === 1) {
            timeAgo = '1 hour ago';
        } else if (hours > 1) {
            timeAgo = `${hours} hours ago`;
        } else if (minutes === 1) {
            timeAgo = '1 minute ago';
        } else if (minutes > 1) {
            timeAgo = `${minutes} minutes ago`;
        } else if (seconds <= 1) {
            timeAgo = 'just now';
        } else {
            timeAgo = `${seconds} seconds ago`;
        }

        element.textContent = timeAgo; // Cập nhật nội dung của div với khoảng thời gian tính được
    });
});
