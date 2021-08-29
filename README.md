# Open Hack_U2021 大都会東小金井

## 初期作業

1. リポジトリをクローンをする(github上の作業場を自分のローカルにコピーする)
```
git clone https://github.com/hayama17/Hack_U
```
2. gitにリモートのリポジトリを登録する
```
git remote add リモートリポジトリの別名 https://github.com/hayama17/Hack_U.git
```

## 作業する前に必ずする事
1. pullを行いローカルのリポジトリを最新の状態にする
```
git pull
```


## push する
```偽t
git add 追加したいファイル
git commit -m "first commit"
git push -u リモートリポジトリの別名 自分の作成したブランチ名(mainに入れる場合は書かなくてOK)
```
## javaの環境構築
1. javaのSDK最新版を[ダウンロード](https://www.oracle.com/java/technologies/javase-jdk16-downloads.html)
   1. 解凍して好きな所に置いてください
2. mavenを[ダウンロード](https://maven.apache.org/download.cgi)
   1. 解凍して好きな所に置いてください
3. javaFXのSDK最新版を[ダウンロード](https://openjfx.io/)
   1. 解凍して好きな所に置いてください
5. vscodeの拡張機能でJava Extension Packをインストール
6. vscodeにSDKのパスを登録　
   1. 設定からjava.homeと検索
   2. Json開いて、java.homeを追加
   ```json
   例
    ~色々書いてある~
    "java.home": "C:\\Program Files\\Java\\jdk-16.0.2"//pathはbinフォルダの上のフォルダまでで良い
    
   ```
   * ，で区切るから一個上の行の最後に","を忘れず
   * 最後は,要らない
  
5. mavenのパスを登録
   1. 設定からmaven.Executable.pathと検索
   2. これは設定画面から直接記入できるはず
    ```cmd
    例
    C:\Program Files\apache-maven-3.8.2\bin\mvn.cmd
    ```


## javaアプリケーションの実行方法
```bash
$ java  -jar --module-path "javaFxのライブラリのパス" --add-modules javafx.controls .\target\test-jar-with-dependencies.jar

ex)羽山公平
> java  -jar --module-path "C:\Program Files\openjfx-11.0.2_windows-x64_bin-sdk\javafx-sdk-11.0.2\lib" --add-modules javafx.controls .\target\test-jar-with-dependencies.jar
```


maven出来なかったら連絡下さい。
## seri

## Mia
下にkaeさん入れたらいいんじゃないかな

## kae

## hamihami

## 共有ドライブ
承認された人のみしかアクセスできません
[リンクをクリック](https://drive.google.com/drive/u/1/folders/14G-73OoaiTKY3rF-kL4ixnEVWRiCD4Vy)
