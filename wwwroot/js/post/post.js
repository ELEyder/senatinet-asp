import { firebaseApp } from '../firebaseApp.js'
const db = firebaseApp.firestore();
function reLoadPosts() {
    const ref_posts = db.collection('posts');

    ref_posts.onSnapshot((snapshot) => {
        snapshot.forEach((doc) => {
            const data = doc.data();
            loadPostHtml(data);
        });
    });
}
async function loadPosts() {
    const ref_posts = db.collection('posts');

    try {
        const snapshot = await ref_posts.get();
        snapshot.forEach((doc) => {
            const data = doc.data();
            loadPostHtml(data);
        });
    } catch (error) {
        console.error('Error fetching posts:', error);
    }
}

function loadPostHtml(data) {
    document.get
    const html = ''
    console.log(data.content);
}

loadPosts();
reLoadPosts();