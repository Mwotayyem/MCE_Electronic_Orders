// جلب العناصر من واجهة المستخدم
const companySelect = document.getElementById('company');
const phoneContainer = document.getElementById('phoneContainer');
const phoneInput = document.getElementById('phone');
var input = document.getElementById('materialnumber');
//var lblvalue = document.getElementById('material1');
const v_ItemName = document.getElementById('ItemName');
const quantityInput = document.getElementById('quantity');
const materialnumberInput = document.getElementById('materialnumber');
const materialTable = document.getElementById('materialTable').getElementsByTagName('tbody')[0];

// البيانات المؤقتة
let materialsData = [];

// العنصر المُعدّ لتخزين التسلسل
let sequence = 1;

var myfun = function (e) {
    if (e.target.value == 123) { v_ItemName.textContent = "test"; }
    else if (e.target.value == 1234) { v_ItemName.textContent = "test2"; }
    else if (e.target.value == 12345) { v_ItemName.textContent = "test3"; }
    else if (e.target.value == 123456) { v_ItemName.textContent = "test4"; }
    else if (e.target.value == 123457) { v_ItemName.textContent = "test5"; }
    else if (e.target.value == 123458) { v_ItemName.textContent = "test6"; }
    else if (e.target.value == 123459) { v_ItemName.textContent = "test7"; }
    else if (e.target.value == 1234510) { v_ItemName.textContent = "test8"; }
}
// إظهار رقم الهاتف عند اختيار الشركة
function showPhoneNumber() {
    const selectedCompany = companySelect.value;
    if (selectedCompany === 'company1') {
        phoneInput.value = '0795225222';
    } else if (selectedCompany === 'company2') {
        phoneInput.value = '07852422522';
    }
    else if (selectedCompany === 'company3') {
        phoneInput.value = '0777511441';
    }
    phoneContainer.classList.remove('hidden');
}

// إضافة مادة إلى الجدول
function addMaterial() {
    //const v_ItemName = v_ItemName.textContent;
    const quantity = quantityInput.value;
    //const materialnumber = materialnumberInput.value;
    if (!materialnumber || !quantity) {
        alert('يرجى ملء جميع الحقول.');
        return;
    }
    if (quantity <= 0) { alert('لا يجوز ان تكون الكمية صفر أو أقل'); }
    else {
        materialsData.push({
            sequence: sequence++,
            materialnumber: materialnumberInput.value,
            material: v_ItemName.textContent,
            quantity: quantityInput.value,
        });}
 
    v_ItemName.textContent = null;
    materialnumber.value = null;
    quantityInput.value = null;
    renderMaterialTable();
}

// عرض بيانات المواد في الجدول
function renderMaterialTable() {
    // إفراغ الجدول
    materialTable.innerHTML = '';
    var Seq = 1;

    // إعادة ملء الجدول
    materialsData.forEach((data) => {
        const row = materialTable.insertRow();
        row.insertCell().textContent = Seq;
        row.insertCell().textContent = data.materialnumber;
        row.insertCell().textContent = data.material;
        row.insertCell().textContent = data.quantity;
        row.insertCell().innerHTML = '<img src="img/' + Seq + '.png" style="width: 51px" />';

        const cancelButton = document.createElement('button');
        cancelButton.textContent = 'الغاء';
        cancelButton.onclick = function () {
            removeMaterial(data.sequence);
        };
        row.insertCell().appendChild(cancelButton);
        Seq = Seq + 1;
    });
    CheckBtnSave();
}

// إلغاء مادة
function removeMaterial(sequence) {
    materialsData = materialsData.filter((data) => data.sequence !== sequence);
    renderMaterialTable();
}

// حفظ البيانات والانتقال للقسم الأول
function saveData() {
    // قم هنا بتنفيذ اللازم لحفظ البيانات ونقل المؤشر للقسم الأول
    // في هذا المثال المؤقت، نقوم بإعادة تعيين البيانات المؤقتة
    materialsData = [];
    sequence = 1;
    materialnumberInput.value = '';
    v_ItemName.value = '';
    quantityInput.value = '';
    renderMaterialTable();
   alert('تم حفظ البيانات بنجاح!');
    CheckBtnSave();
}

 function CheckBtnSave() {
    var table = document.getElementById("materialTable");
    var saveButton = document.getElementById("BtnSave");

    if (table.rows.length > 1) { // يعتبر هذا الرقم هو عدد الصفوف بداخل الجدول
        saveButton.disabled = false; // تفعيل زر الحفظ إذا كان هناك بيانات في الجدول
    } else {
        saveButton.disabled = true; // تعطيل زر الحفظ إذا لم يكن هناك بيانات في الجدول
    }
};