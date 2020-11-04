console.log("started server...");

// get user video recording
// show preview
// download video or upload to cloud
// POST video link to videoAsk
// get videos from VA
// display those videos

let constraints: MediaStreamConstraints = {
  audio: true,
  video: {
    width: { ideal: 1280 },
    height: { ideal: 720 },
  },
};

let video1: HTMLMediaElement = <HTMLVideoElement>(
  document.getElementById("video1")!
);
let video2: HTMLVideoElement = <HTMLVideoElement>(
  document.getElementById("video2")!
);
let downloadButton: HTMLAnchorElement = <HTMLAnchorElement>(
  document.getElementById("downloadBtn")
);
let startRecordingButton = document.getElementById("startRecordingBtn")!;

let dataRecorded: Blob[] = [];
let mediaRecorder: MediaRecorder;
let localStream: MediaStream;

startRecordingButton.addEventListener("click", () => {
  if (startRecordingButton.textContent === "Start Recording") {
    //startRecording(constraints);
    navigator.mediaDevices
      .getUserMedia(constraints)
      .then(handleSuccess)
      .catch(handleError);
  } else {
    stopRecording(<MediaStream>video1.srcObject);
  }
});

function handleSuccess(stream: MediaStream) {
  console.log("Starting to record...");

  video1.srcObject = stream;
  mediaRecorder = new MediaRecorder(stream, { mimeType: "video/webm" });

  mediaRecorder.ondataavailable = (event) => dataRecorded.push(event.data);

  mediaRecorder.onstop = function () {
    let recordedBlob = new Blob(dataRecorded, { type: "video/webm" });
    let videoURL = URL.createObjectURL(recordedBlob);
    video2.src = videoURL;
    dataRecorded.length = 0;

    downloadButton.download = "AzuloRecording.webm";
    downloadButton.href = videoURL;

    startRecordingButton.textContent = "Start Recording";
  };

  mediaRecorder.start();
  startRecordingButton.textContent = "Stop Recording";
}

function handleError(error: string) {
  console.log("Error: ", error);
}

function stopRecording(stream: MediaStream) {
  console.log("stopping recording...");
  mediaRecorder.stop();
  stream.getTracks().forEach((el) => el.stop());
}

let tempToken =
  "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ik1UTkJRamhFUmpZd05VRXlRakpFUkRGRk5rSXpPRGc0T0RZMlFqWTNSamd3TURoRVFUVTROZyJ9.eyJodHRwczovL3ZpZGVvYXNrLmNvbS9sb2dpbnNfY291bnQiOjEsImh0dHBzOi8vdmlkZW9hc2suY29tL2lzX3NpZ251cCI6ZmFsc2UsImh0dHBzOi8vdmlkZW9hc2suY29tL2NyZWF0ZWRfdXNlciI6ZmFsc2UsImlzcyI6Imh0dHBzOi8vYXV0aC52aWRlb2Fzay5jb20vIiwic3ViIjoiYXV0aDB8NWZhMDFjZTE3MjBlNWYwMDZkNzJjMjhiIiwiYXVkIjpbImh0dHBzOi8vYXBpLnZpZGVvYXNrLmNvbS8iLCJodHRwczovL3ZpZGVvYXNrLmV1LmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE2MDQ0NjYyMDEsImV4cCI6MTYwNDQ3MzQwMSwiYXpwIjoicDNNbTM4alJpZGVoU01NT0E5N2xUdkoyN1BDbm5HSmgiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIn0.lnd24z3UZOBzJWJH6D5fKcrFEDzxJI5eJDLu_d-BAaWNctvPE_S05bz-Y7uCv4uB33k16duC9Mfxa1awVHKk8WXBAXQeGoZySlJmQCxonJyRdA8kubWyAsFW_HaP42sjbNswVz4FEhDjqKAiRqfHCPi4wrDT5pmHChitZHisbBX04WUy8Xso5KSA2uBZ89xGATO1pB9WPkxalitSYDhgeUM3DChn_Sg-oOOWWuIqQ5ODTBnJC8o6m8oOFQ3bnazKWxwg_IljGbZha3PS1OwlDMI8myM6cC5zUiJQlkpKTE9l5b1Tx6_AY0dmJq0puqdct2kkKC4t9XLIQxdBALjuvQ";
let clientId;
let AuthURL;
let redirectUri;
let scopes;
let formsURL = "https://api.videoask.com/forms";
let questionsURL = "https://api.videoask.com/questions";
let testVideoMP4 =
  "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerMeltdowns.mp4";
let testVideoWebm = "http://dl5.webmfiles.org/big-buck-bunny_trailer.webm";

let getDataBtn: HTMLButtonElement = <HTMLButtonElement>(
  document.getElementById("getVideosBtn")
);
getDataBtn.addEventListener("click", () => {
  console.log("Getting data from VA...");
  getData();
});

let createFormBtn: HTMLButtonElement = <HTMLButtonElement>(
  document.getElementById("createFormBtn")
);
createFormBtn.addEventListener("click", () => {
  console.log("Uploading form to VA...");
  createForm();
});

let uploadVideoBtn: HTMLButtonElement = <HTMLButtonElement>(
  document.getElementById("uploadVideoBtn")
);
uploadVideoBtn.addEventListener("click", () => {
  console.log("Uploading video to VA...");
  uploadVideo();
});

// get forms
function getData() {
  let videoAskheaders: Headers = new Headers();
  videoAskheaders.append("Authorization", "Bearer " + tempToken);

  let fetchData = {
    method: "GET",
    headers: videoAskheaders,
  };

  fetch(formsURL, fetchData)
    .then((res) => {
      res.json().then((resJson) => {
        console.log(resJson);
      });
    })
    .catch((e) => {
      console.log("Error getting data");
    });
}

let formIDtest:string;

// create Form
function createForm() {
  let videoAskheaders: Headers = new Headers();
  videoAskheaders.append("Authorization", "Bearer " + tempToken);
  videoAskheaders.append("Content-Type", "application/json");

  let formBody = {
    title: "My VideoAsk",
    show_contact_name: false,
    show_contact_email: false,
    show_contact_phone_number: false,
    show_consent: false,
    requires_contact_name: false,
    requires_contact_email: false,
    requires_contact_phone_number: false,
    requires_consent: false,
  };

  let postData:RequestInit = {
    method: "POST",
    body: JSON.stringify(formBody),
    headers: videoAskheaders,
  };

  fetch(formsURL, postData)
    .then((res) => {
      res.json().then((resJson) => {
        console.log(resJson);
        formIDtest = resJson['form_id'];
        console.log('Form id is: ' + formIDtest);
      });
    })
    .catch((e) => {
      console.log("Error posting form");
    });
}


// upload Question
function uploadVideo() {
  let videoAskheaders: Headers = new Headers();
  videoAskheaders.append("Authorization", "Bearer " + tempToken);
  videoAskheaders.append("Content-Type", "application/json");

  let questionBody = {
    "form_id": formIDtest,
    "media_type": "video",
    "media_url": testVideoWebm,
    "thumbnail_url": "https://media3.giphy.com/media/du50vJjfVwziQkzqCs/giphy-preview.gif",
    "allowed_answer_media_types": [
      "video",
      "audio",
      "text"
    ]
  }

  let postData = {
    method: 'POST',
    body: JSON.stringify(questionBody),
    headers: videoAskheaders,
  };
  fetch(questionsURL, postData)
    .then((res) => {
      res.json().then((resJson) => {
        console.log(resJson);
      });
    })
    .catch((e) => {
      console.log("Error posting video question");
    });
}
