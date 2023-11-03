document.querySelectorAll("select[data-fill-endpoint]").forEach((select) => {
    let endpoint = select.dataset["fillEndpoint"];
    let parameter = select.dataset["fillParameter"];
    let target = select.dataset["fillDataTarget"];
    let targetElement = document.getElementById(target);
    
    if (parameter === undefined && target === undefined) return;
    
    let url = new URL(endpoint, window.location.origin);

    let callbackID = 0;
    function debounce(callback, delay = 500){
        clearTimeout(callbackID);
        callbackID = setTimeout(() => {
            callback();
        }, delay);
    }    

    targetElement.addEventListener("change", () => {
        debounce(() => {
            fillSelect(select,targetElement, parameter, url);
        });
    });
});

/**
 * @param {Element} select
 * @param {Element} target
 * @param {string} parameter
 * @param {URL} url
 */
function fillSelect(select,target,parameter,url) {
    console.log(target.value)
    if (!target.value) {
        setOptions(select, null);
        return;
    }
    
    url.searchParams.set(parameter, target.value);
    fetch(url.toString()).then(r => r.json()).then(data => {
        setOptions(select, data);
    });
}

function setOptions(select, data){
    console.table(data)
    select.innerHTML = "";
    let option = document.createElement("option");
    option.value = "";
    option.innerText = data ? "None Selected" : "No Data";
    select.appendChild(option);

    if (data === null || data.length === 0) return;
    
    data.forEach((item) => {
        let option = document.createElement("option");
        option.value = item.id;
        option.innerText = item.name;
        select.appendChild(option);
    });
}