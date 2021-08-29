package com.example;

import java.io.IOException;
import java.util.ArrayList;
import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.CheckBox;
import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.MenuItem;

import javafx.scene.control.ToggleGroup;
import javafx.scene.layout.BorderPane;

import javafx.scene.layout.VBox;

import javafx.stage.Stage;

public class App extends Application {

    CheckBox cbwebhook, cbzoom;
    Button bt1;
    MenuBar mb;
    Menu mn = new Menu();
    MenuItem[] mi = new MenuItem[3];

    ToggleGroup[] tg;

    public static void main(String[] args) {
        launch(args);
    }

    @Override
    public void start(Stage primaryStage) throws Exception {
        // TODO 自動生成されたメソッド・スタブ

        mb = new MenuBar();
        mn = new Menu("ファイル");
        mi[0] = new MenuItem("import");
        mi[1] = new MenuItem("export");
        mn.getItems().addAll(mi[0], mi[1]);
        mb.getMenus().add(mn);

        cbwebhook = new CheckBox("Web hook");
        cbzoom = new CheckBox("start zoom");

        bt1 = new Button("start");
        bt1.setOnAction(new EventStart());
        bt1.setAlignment(Pos.CENTER);
        bt1.setMaxWidth(Double.MAX_VALUE);

        VBox vb = new VBox();
        vb.getChildren().addAll(cbwebhook, cbzoom);
        vb.setAlignment(Pos.CENTER);

        BorderPane bp = new BorderPane();
        bp.setPadding(new Insets(0, 0, 0, 0));
        bp.setTop(mb);
        bp.setCenter(vb);

        bp.setBottom(bt1);

        Scene sc = new Scene(bp, 200, 100);

        primaryStage.setScene(sc);
        primaryStage.setTitle("RandomNumGenerator_FX");
        primaryStage.show();

    }

    class EventStart implements EventHandler<ActionEvent> {

        @Override
        public void handle(ActionEvent event) {
            // TODO 自動生成されたメソッド・スタブ
            ArrayList<String> path = new ArrayList<String>();
            if (cbwebhook.isSelected()) {
                // Pythonのコード実行
                path.add(Thread.currentThread().getContextClassLoader().getResource("webhook.py").getPath());
            }
            if (cbzoom.isSelected()) {
                // pythonのコード実行
                path.add(Thread.currentThread().getContextClassLoader().getResource("Auto_zoom_start.py").getPath());
            }
            for (int i = 0; i < path.size(); i++) {
                ProcessBuilder processBuilder = new ProcessBuilder("py", path.get(i).substring(3));
                try {
                    processBuilder.start();
                } catch (IOException e) {
                    // TODO Auto-generated catch block
                    e.printStackTrace();
                }
            }
        }
    }
}
