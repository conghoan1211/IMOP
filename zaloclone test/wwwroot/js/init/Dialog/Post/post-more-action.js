function initPostMoreModal() {
  const postMoreButtons = document.querySelectorAll(".aprofile-post-more"); // Các nút "post more"
  const modals = document.querySelectorAll("#modalPostAction"); // Các modal tương ứng
  const profileMoreButton = document.querySelector(".aprofile-friends-more"); // Nút "friends more"
  const profileModal = document.querySelector("#modalProfileAction"); // Modal của "friends more"
  const otherProfileBtn = document.querySelector("#openOtherProfileDialog"); // Nút "messages more"
  const otherProfileModal = document.querySelector("#profileOtherDialog"); // Modal của "messages more"

  function toggleModal(modal) {
    modal.style.display = modal.style.display === "none" ? "flex" : "none";
  }

  function hideAllModals() {
    [...modals, profileModal, otherProfileModal].forEach((modal) => {
      if (modal && modal.style.display === "flex") {
        modal.style.display = "none";
      }
    });
  }

  // Hiển thị modal khi click vào nút "post more"
  postMoreButtons.forEach((button, index) => {
    const modal = modals[index];
    button.addEventListener("click", function (event) {
      event.stopPropagation();
      hideAllModals();
      toggleModal(modal);
    });
  });

  // Hiển thị modal khi click vào nút "friends more"
  if (profileMoreButton && profileModal) {
    profileMoreButton.addEventListener("click", function (event) {
      event.stopPropagation();
      hideAllModals();
      toggleModal(profileModal);
    });
  }

  // Hiển thị modal khi click vào nút "messages more"
  if (otherProfileBtn && otherProfileModal) {
    otherProfileBtn.addEventListener("click", function (event) {
      event.stopPropagation();
      hideAllModals();
      toggleModal(otherProfileModal);
    });
  }

  // Ẩn modal khi click ra ngoài
  document.addEventListener("click", function () {
    hideAllModals();
  });

  // Ngăn modal bị ẩn khi click vào chính modal đó
  [...modals, profileModal, otherProfileModal].forEach((modal) => {
    if (modal) {
      modal.addEventListener("click", function (event) {
        event.stopPropagation();
      });
    }
  });

  // Đóng modal bằng phím "Esc"
  document.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
      hideAllModals();
    }
  });
}

// Export or attach to global object if not using modules
if (typeof module !== "undefined" && module.exports) {
  module.exports = initPostMoreModal;
} else {
  window.initPostMoreModal = initPostMoreModal;
}
