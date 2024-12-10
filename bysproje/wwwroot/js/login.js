document.addEventListener('DOMContentLoaded', function () {
    const studentTab = document.querySelector('.tab.student');
    const personnelTab = document.querySelector('.tab.personnel');
    const loginForm = document.querySelector('.login-form');

    // Tab seçimini kontrol etme
    studentTab.addEventListener('click', function () {
        setActiveTab('student');
    });

    personnelTab.addEventListener('click', function () {
        setActiveTab('personnel');
    });

    // Sekmeyi aktif yapma
    function setActiveTab(type) {
        if (type === 'student') {
            studentTab.classList.add('active');
            personnelTab.classList.remove('active');
            loginForm.action = '/Student/StudentDashboard'; // Öğrenci form action
        } else if (type === 'personnel') {
            personnelTab.classList.add('active');
            studentTab.classList.remove('active');
            loginForm.action = '/Personnel/PersonnelDashboard'; // Personel form action
        }
    }

    // Sayfa yüklenirken aktif sekmeyi ayarla
    if (studentTab.classList.contains('active')) {
        setActiveTab('student');
    } else if (personnelTab.classList.contains('active')) {
        setActiveTab('personnel');
    }
});
