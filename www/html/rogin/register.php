<?php
//フォームからの値をそれぞれ変数に代入
$name = $_POST['name'];
$mail = $_POST['mail'];
$pass = password_hash($_POST['pass'], PASSWORD_DEFAULT);
$dsn = 'mysql:host=db;dbname=test;';
$username = "docker_user";
$password = "docker_pass";
try {
    $dbh = new PDO($dsn, $username, $password);
} catch (PDOException $e) {
    $msg = $e->getMessage();
}

//フォームに入力されたmailがすでに登録されていないかチェック
try {
    $sql = "SELECT * FROM account WHERE mail = :mail";
    $stmt = $dbh->prepare($sql);
    $stmt->bindValue(':mail', $mail);
    $stmt->execute();
    $member = $stmt->fetch();
    if ($member['mail'] === $mail) {
        $msg = '同じメールアドレスが存在します。';
        $link = '<a href="signup.php">戻る</a>';
    } else {
        //登録されていなければinsert   
        $sql = "INSERT INTO account (`name`, `mail`,`PWD`) VALUES (:name, :mail, :pass)";
        $stmt = $dbh->prepare($sql);
        $stmt->bindValue(':name', $name);
        $stmt->bindValue(':mail', $mail);
        $stmt->bindValue(':pass', $pass);
        $msg2 = $stmt ->tostr;
        $stmt->execute();
        $msg = '会員登録が完了しました';
        $link = '<a href="login.php">ログインページ</a>';
    }
} catch (PDOException $e) {
    die($e->getMessage());
}



?>

<h1><?php echo $msg; ?></h1>
<!--メッセージの出力-->
<?php echo $pass; ?>
<?php echo $link; ?>