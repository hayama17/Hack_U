<?php
session_start();
$username = $_SESSION['name'];
if (isset($_SESSION['id'])) {//ログインしているとき
    $msg = 'こんにちは' . htmlspecialchars($username, \ENT_QUOTES, 'UTF-8') . 'さん';
    $link = '<a href="logout.php">ログアウト</a>';
} else {//ログインしていない時
    $msg = 'ログインしていません';
    $link = '<a href="login_form.php">ログイン</a>';
    $msg2 = '新規会員登録';
    $link2 = '<a href="signup.php">新規会員登録</a>';
}
?>
<h1><?php echo $msg; ?></h1>
<?php echo $link; ?>
<h2><?php echo $msg2; ?></h2>
<?php echo $link2; ?>