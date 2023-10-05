// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets
let toggler = document.getElementById("ThemeToggler")
let root = document.getElementsByTagName("html")[0];

function setTheme(isDark){
    //hello
    console.log("setting theme")
    if (isDark) {
        console.log("setting dark")
        toggler.classList.remove("fa-sun");
        toggler.classList.add("fa-moon");
        root.setAttribute("data-bs-theme", "dark");
    } else {
        toggler.classList.remove("fa-moon");
        toggler.classList.add("fa-sun");
        root.setAttribute("data-bs-theme", "light");
    }
}

window.addEventListener("load", (event) => {

});


// Write your JavaScript code.
toggler.addEventListener("click", function (e) {
    setTheme(root.getAttribute("data-bs-theme") === "light")
});

