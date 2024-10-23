document.addEventListener("DOMContentLoaded", function () {
  const postActionBtns = document.querySelectorAll(".post-react-heart");

  postActionBtns.forEach((btn) => {
    const regularHeart = btn.querySelector(".fa-regular.fa-heart");
    const solidHeart = btn.querySelector(".fa-solid.fa-heart");
    const countSpan = btn.querySelector("span");

    let liked = false; // Track the like status

    btn.addEventListener("click", function () {
      liked = !liked; // Toggle like status

      if (liked) {
        regularHeart.style.display = "none";
        solidHeart.style.display = "inline";
        countSpan.textContent = parseInt(countSpan.textContent) + 1; // Increase count
        btn.classList.add("active"); // Add active class to change color
      } else {
        solidHeart.style.display = "none";
        regularHeart.style.display = "inline";
        countSpan.textContent = parseInt(countSpan.textContent) - 1; // Decrease count
        btn.classList.remove("active"); // Remove active class to reset color
      }
    });
  });
});

//document.addEventListener("DOMContentLoaded", function () {
//    // Attach event listeners to all like buttons
//    const likeButtons = document.querySelectorAll(".aprofile-post-action__btn");

//    likeButtons.forEach(button => {
//        button.addEventListener("click", function () {
//            // Get the parent form element (which contains the data-post-id attribute)
//            const form = button.closest("form");
//            const postId = form.getAttribute("data-post-id");

//            if (postId) {
//                // Make a fetch request to the server with the correct route
//                fetch(`/Post?handler=ToggleLike&postId=${encodeURIComponent(postId)}`, { method: 'POST' })
//                    .then(response => {
//                        if (response != null)  {
//                            throw new Error("Failed to like post");
//                        }
//                        return response.json();
//                    })
//                    .then(data => {
//                        console.log("Post liked successfully", data);
//                    })
//                    .catch(error => {
//                        console.error("Error liking post:", error);
//                    });
//            } else {
//                console.error("postId is null or undefined");
//            }
//        });
//    });
//});

