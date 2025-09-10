window.camera = {
  start: async function() {
    const video = document.getElementById('cam');
    if (!video) return;
    const stream = await navigator.mediaDevices.getUserMedia({ video: true, audio: false });
    video.srcObject = stream;
  },
  captureJpeg: function() {
    const video = document.getElementById('cam');
    const canvas = document.createElement('canvas');
    canvas.width = video.videoWidth || 480;
    canvas.height = video.videoHeight || 360;
    const ctx = canvas.getContext('2d');
    ctx.drawImage(video, 0, 0, canvas.width, canvas.height);
    const dataUrl = canvas.toDataURL('image/jpeg', 0.92);
    // convert to byte[]
    const byteString = atob(dataUrl.split(',')[1]);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const intArray = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) intArray[i] = byteString.charCodeAt(i);
    return Array.from(intArray);
  }
};
