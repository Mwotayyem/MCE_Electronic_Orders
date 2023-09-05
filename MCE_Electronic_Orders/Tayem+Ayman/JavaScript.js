const testButton = document.getElementById('testButton');
const resultParagraph = document.getElementById('result');

testButton.addEventListener('click', () => {
    const startTime = performance.now();
    fetch('https://www.example.com/some-small-file.jpg')
        .then(() => {
            const endTime = performance.now();
            const downloadTime = endTime - startTime;
            const speedMbps = calculateSpeed(downloadTime);
            resultParagraph.textContent = `تم تنزيل الملف بنجاح. السرعة التقريبية: ${speedMbps.toFixed(2)} Mbps`;
        })
        .catch(() => {
            resultParagraph.textContent = 'حدث خطأ أثناء التنزيل. يرجى المحاولة مرة أخرى.';
        });
});

function calculateSpeed(downloadTime) {
    // احتساب السرعة بناءً على حجم الملف ووقت التنزيل
    // يمكن استخدام هذه المعادلة للتقدير: السرعة (بالميجابت في الثانية) = حجم الملف (بالبت) / وقت التنزيل (بالثواني)
    // يُفضّل استخدام ملف صغير للحصول على تقدير تقريبي أدق للسرعة

    const fileSizeBits = 100000; // حجم الملف بالبت
    const downloadTimeSeconds = downloadTime / 1000; // وقت التنزيل بالثواني
    const speedMbps = fileSizeBits / downloadTimeSeconds / 1000000; // السرعة بالميجابت في الثانية

    return speedMbps;
}