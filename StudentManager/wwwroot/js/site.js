// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("ThemeToggler").addEventListener("click", function (e) {
    let root = document.getElementsByTagName("html")[0];
    if (root.getAttribute("data-bs-theme") === "dark") {
        e.target.classList.remove("fa-sun");
        e.target.classList.add("fa-moon");
        root.setAttribute("data-bs-theme", "light");
    } else {
        e.target.classList.remove("fa-moon");
        e.target.classList.add("fa-sun");
        root.setAttribute("data-bs-theme", "dark");
    }
});