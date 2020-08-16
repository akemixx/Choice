async function SaveDiscChanges() {
    const url = "/StudDiscs/FormSubmitAjax";
    var disciplines = [];
    var elems = document.getElementsByTagName("tr");
    for (let i = 1; i < elems.length; i++) {
        disciplines.push(
            {
                "Id": elems[i].lastElementChild.firstElementChild.value, // id
                "Title": elems[i].children[0].innerText, // title
                "Selected": elems[i].children[1].firstElementChild.checked, // checkbox
            }
        );
    }
    let data = JSON.stringify(disciplines);
    let token = document.getElementsByName("__RequestVerificationToken")[0].value;

    try {
        const response = await fetch(url, {
            method: 'POST',
            credentials: 'include',
            headers: {
                "XSRF-TOKEN": token,
                "Content-Type": "application/json"
            },
            body: data
        });
        if (response.status == 200) {
            const json = await response.json();
            RenewSelectionData(disciplines);
            alert("Successfully saved!");
        }
        else {
            alert("Error");
        }
    } catch (error) {
        console.error("Error:", error);
    }
}

function RenewSelectionData(disciplines) {
    var html = "<tr><th>Title</th><th></th><th></th></tr>";
    for (let i = 0; i < disciplines.length; i++) {
        html += "<tr><td>" +
            disciplines[i]["Title"] +
            "</td><td style='text-align:center'>";
        if (disciplines[i]["Selected"]) {
            html += "<input checked='checked' type='checkbox' value=" +
                disciplines[i]["Selected"] +
                "></td>";
        }
        else {
            html += "<input type='checkbox' value=" +
                disciplines[i]["Selected"] +
                "></td>";
        }
        html += "<td><input type='hidden' value=" +
            disciplines[i]["Id"] +
            "></td></tr>";
    }
    document.getElementsByTagName("tbody")[0].innerHTML = html;
}

