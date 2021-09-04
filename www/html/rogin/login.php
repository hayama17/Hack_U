<?php
session_start();
$mail = $_POST['mail'];
$dsn = 'mysql:host=db;dbname=test;';
$username = "docker_user";
$password = "docker_pass";
try {
    $dbh = new PDO($dsn, $username, $password);
} catch (PDOException $e) {
    $msg = $e->getMessage();
}

$sql = "SELECT * FROM account WHERE mail = :mail";
$stmt = $dbh->prepare($sql);
$stmt->bindValue(':mail', $mail);
$stmt->execute();
$member = $stmt->fetch();
//指定したハッシュがパスワードにマッチしているかチェック
if (password_verify($_POST['pass'], $member['PWD'])) {
    //DBのユーザー情報をセッションに保存
    $_SESSION['id'] = $member['ID'];
    $_SESSION['name'] = $member['name'];
    $msg = 'ログインしました。';
    $link = '<a href="index.php">ホーム</a>';
} else {
    $msg = 'メールアドレスもしくはパスワードが間違っています。';
    $link = '<a href="login_form.php">戻る</a>';
    $link2 = '<a href="index.php">ホーム</a>';
}
?>

<h1><?php echo $msg; ?></h1>
<?php echo $link; ?>
<?php echo $link2; ?>