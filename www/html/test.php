<?php
try {
    // host=XXXの部分のXXXにはmysqlのサービス名を指定します
    $dsn = 'mysql:host=db;dbname=test;';
    $username = "docker_user";
    $password = "docker_pass";
    $name = "hym";
    $mail = "kohei.hayama.9r@stu.hosei.ac.jp";
    $dbh = new PDO($dsn, $username, $password);
    $sql = "SELECT * FROM account WHERE mail = :mail";
    $stmt = $dbh->prepare($sql);
    $stmt->bindValue(':mail', $mail);
    $stmt->execute();
    $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
    var_dump($result);
    $member =  $stmt->fetch();
    echo $member['PWD']
} catch (PDOException $e) {
    echo $e->getMessage();
    exit;
}
