document.addEventListener('DOMContentLoaded', () => {
    const rows = document.querySelectorAll('.row');

    rows.forEach(row => {
        const buttons = row.querySelectorAll('.btnChoose');

        buttons.forEach(button => {
            button.addEventListener('click', () => {
                const rowClass = row.classList[1]; // Get the row class name (e.g., row1, row2, ...)
                let inputId = '';

                switch (rowClass) {
                    case 'row1':
                        inputId = 'PaperType';
                        break;
                    case 'row2':
                        inputId = 'Size';
                        break;
                    case 'row3':
                        inputId = 'BookCuffColor';
                        break;
                    case 'row4':
                        inputId = 'NumberOfPages';
                        break;
                }

                if (inputId) {
                    document.getElementById(inputId).value = button.value;
                    updateActiveClass(row, button);
                }
            });
        });
    });

    document.getElementById('selectImageFrontButton').addEventListener('click', () => {
        document.getElementById('fileInputFront').click();
    });

    document.getElementById('selectImageBackButton').addEventListener('click', () => {
        document.getElementById('fileInputBack').click();
    });

    document.getElementById('fileInputFront').addEventListener('change', event => handleFileSelect(event, 'ImageFrontPath', 'front'));
    document.getElementById('fileInputBack').addEventListener('change', event => handleFileSelect(event, 'ImageBackPath', 'back'));
});

function handleFileSelect(event, inputId, imageType) {
    const file = event.target.files[0]; // Get the selected image file
    if (file) {
        sendImageToServer(file, inputId, imageType);
    } else {
        alert('Please select an image file.');
    }
}

function sendImageToServer(file, inputId, imageType) {
    const formData = new FormData();
    formData.append('image', file);
    formData.append('imageType', imageType); // Send image type with file

    let endpoint = '/Choose/UploadFrontImages';
    if (imageType === 'back') {
        endpoint = '/Choose/UploadBackImages';
    }

    fetch(endpoint, {
        method: 'POST',
        body: formData,
    })
        .then(response => {
            if (response.ok) {
                return response.json(); // Parse response JSON
            } else {
                throw new Error('Failed to upload images.');
            }
        })
        .then(data => {
            document.getElementById(inputId).value = data.imagePath;
            alert('Images uploaded successfully.');
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Failed to upload images.');
        });
}

function updateActiveClass(row, activeButton) {
    row.querySelectorAll('.btnChoose').forEach(btn => {
        btn.classList.remove('active');
    });

    activeButton.classList.add('active');
}


/*============================== ADDRESS =====================================*/

// 1. what is API
// 2. How do I call API
// 3. Explain code
const host = "https://provinces.open-api.vn/api/";
var callAPI = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data, "province");
        });
}
callAPI('https://provinces.open-api.vn/api/?depth=1');
var callApiDistrict = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.districts, "district");
        });
}
var callApiWard = (api) => {
    return axios.get(api)
        .then((response) => {
            renderData(response.data.wards, "ward");
        });
}

var renderData = (array, select) => {
    let row = ' <option disable value="">chọn</option>';
    array.forEach(element => {
        row += `<option value="${element.code}">${element.name}</option>`
    });
    document.querySelector("#" + select).innerHTML = row
}

$("#province").change(() => {
    callApiDistrict(host + "p/" + $("#province").val() + "?depth=2");
    printResult();
});
$("#district").change(() => {
    callApiWard(host + "d/" + $("#district").val() + "?depth=2");
    printResult();
});
$("#ward").change(() => {
    printResult();
})

var printResult = () => {
    if ($("#district").val() != "" && $("#province").val() != "" &&
        $("#ward").val() != "") {
        let result = $("#province option:selected").text() +
            " | " + $("#district option:selected").text() + " | " +
            $("#ward option:selected").text();
        $("#result").text(result)
    }

}