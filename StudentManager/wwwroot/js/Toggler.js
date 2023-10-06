// Please see documentation at https://docs.mcd icrosoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets
let isDark = getThemeStorage();
let root = document.getElementsByTagName("html")[0]
/** @type {HTMLHtmlElement} */
let toggle
//set theme on start to prevent theme flickering
setBootstrapTheme(isDark)

//wait for the dom to be loaded
window.addEventListener("load", ()=>{
    //set toggle
    setToggle(isDark);
    toggle = document.getElementById("ThemeToggler");
    
    document.getElementById("ThemeToggler")
        .addEventListener("click", function (_) {
        isDark = !isDark;
        setBootstrapTheme(isDark);
        setToggle(isDark);
        setThemeStorage(isDark);
    });
})

function getThemeStorage(){
    return JSON.parse(localStorage.getItem("theme")) === true;
}
function setThemeStorage(isDark){
    return localStorage.setItem("theme",JSON.stringify(isDark));
}

function setBootstrapTheme(isDark) {
    if (typeof (isDark) !== typeof true)
        isDark = true;

    document.getElementsByTagName("html")[0].dataset["bsTheme"] = (isDark ? "dark" : "light");
}

const iconClass = ["fa-moon", "fa-sun"];
function setToggle(isDark) {
    if (typeof (isDark) !== typeof true)
        isDark = true;

    document.getElementById("ThemeToggler")?.classList.replace(iconClass[+isDark], iconClass[+(!isDark)]);
}


