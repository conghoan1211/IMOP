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