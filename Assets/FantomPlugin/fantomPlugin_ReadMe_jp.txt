http://fantom1x.blog130.fc2.com/blog-entry-273.html
http://fantom1x.blog130.fc2.com/blog-entry-293.html
Android Native Dialogs and Functions Plugin
セットアップ＆ビルド マニュアル

･ネイティブプラグイン "fantomPlugin.aar" は「Minimum API Level：Android 4.2 (API 17)」以上で使用して下さい。

(※) ストレージのテキストファイル読み書き機能「StorageLoadTextController」「StorageSaveTextController」を利用するには「Android 4.4 (API 19)」以上にする必要があります。

(※) センサーの値を取得するには各センサーの要求 API Level 以上にする必要があります。詳細は公式のドキュメントまたは、センサー関連メソッド・定数などのコメントを参照して下さい。
https://developer.android.com/reference/android/hardware/Sensor.html#TYPE_ACCELEROMETER

･"Assets/FantomPlugin/Plugins/" フォルダを "Assets/Plugins/" のように "Assets/" 直下に移動して下さい。この「Plugins」フォルダはランタイムでプラグインを稼働させるための特殊なフォルダとなります。
(参照) https://docs.unity3d.com/ja/current/Manual/ScriptCompileOrderFolders.html

･ハードウェア音量キーのイベント取得、ダイアログ付きの音声認識、WIFIの設定を開く、Bluetooth接続要求（ダイアログ）、ストレージのテキストファイルの読み書き、ギャラリーの画像パス取得、MediaScannerの更新機能、バッテリーのステータス取得、画面回転の変化取得、センサーの値取得、デバイス認証、QRコードスキャナからのテキスト取得を利用する場合には "AndroidManifest-FullPlugin~.xml" を "AndroidManifest.xml" にリネームして使用して下さい。

・使用する機能によっては Android のパーミッションが必要になります（https://developer.android.com/guide/topics/security/permissions.html）。パーミッションについては「Assets/Plugins/Android/Permission_ReadMe.txt」にまとめてあります。必要なパーミッションを「AndroidManifest.xml」にコピペして下さい（利用しない機能のパーミッションは削除する方が好ましいです）。

･テキスト読み上げを使用するには、端末に読み上げエンジンと音声データがインストールされている必要があります。
(テキスト読み上げのインストール)
http://fantom1x.blog130.fc2.com/blog-entry-275.html#fantomPlugin_TextToSpeech_install
(音声データ：Google Play)
https://play.google.com/store/apps/details?id=com.google.android.tts
https://play.google.com/store/apps/details?id=jp.kddilabs.n2tts

・QRコード読み取りを利用するには端末に ZXing（googleのオープンソースプロジェクト）のQRコードスキャナアプリがインストールされている必要があります。インストールされていない場合、インストールを促すダイアログが表示されます（Google Play へ誘導されます）。
(Google Play)
https://play.google.com/store/apps/details?id=com.google.zxing.client.android
(ZXing オープンソースプロジェクト)
https://github.com/zxing/zxing

･"AndroidManifest~.xml"の"_Landscape" または "_Portrait","_Sensor" はアプリの画面回転の属性(screenOrientation)に合わせて選択して下さい。
(参照) https://developer.android.com/guide/topics/manifest/activity-element.html#screen

(※) 警告「Unable to find unity activity in manifest. You need to make sure orientation attribute is set to sensorPortrait manually.」は Unityの標準のアクティビティ(UnityPlayerActivity)以外を使うと出るので無視して下さい。

------------------------------------------------
■デモについて

･デモをビルドするときは "AndroidManifest_demo.xml" を "AndroidManifest.xml" にリネームして使って下さい。また「Build Settings...」に「Assets/FantomPlugin/Demo/Scenes/」にあるシーンを追加して、「Switch Platform」で「Android」にしてビルドして下さい。

※「GalleryPickTest」のデモには全天球のメッシュ（360 degrees）は含まれてません。必要であれば以下のURLから「Sphere100.fbx」ダウンロードし、ヒエラルキーの「GalleryPickTest(Script)」の「Sphere」にセットして下さい。また「Sphere100」の Material に「TextureMat」をセットして下さい。全天球は内側から覗く感じになるので、スケールの X にマイナス値を与えると画像を反転できます（大きさは任意。デモビデオでは10）。

(全天球メッシュ：Sphere100.fbx)
http://warapuri.com/post/131599525953/
(Demo video：Vimeo)
https://vimeo.com/255712215


それではよりよい作品の手助けになることを、心から願っています。

------------------------------------------------
■更新履歴

(ver.1.1)
・ピンチ（PinchInput）,スワイプ（SwipeInput）,ロングタップ（LongClickInput/LongClickEventTrigger）とそのデモシーン（PinchSwipeTest）を追加。
・SmoothFollow3（元は StandardAssets の SmoothFollow）に左右回転アングルと高さと距離の遠近機能を追加し、ピンチ（PinchInput）やスワイプ（SwipeInput）にも対応させた改造版（SmoothFollow3）を追加（デモシーン：PinchSwipeTest で使用）。
・XColor の色形式変換を ColorUtility から計算式(Mathf.RoundToInt())に変更。
・XDebug に行数制限を追加。
(ver.1.2)
・おおよそ全ての機能のプレファブ＆「～Controller」スクリプトを追加。
・単一選択（SingleChoiceDialog）、複数選択（MultiChoiceDialog）、スイッチダイアログ（SwitchDialog）、カスタムダイアログのアイテムに値変化のコールバックを追加。
・XDebug の自動改行フラグ(newline)が無視されていた不具合を修正。また、行数制限を使用しているときに、OnDestory() でテキストのバッファ（Queue）をクリアするようにした。
(ver.1.3)
・WIFIのシステム設定を開く機能（WifiSettingController）を追加。
・Bluetoothの接続要求（ダイアログ表示）をする機能（BluetoothSettingController）を追加。
・アプリChooserを利用してテキストを送信する（簡易的なテキストのシェア）（SendTextController）機能を追加。
・ストレージアクセスフレームワーク（API19以上）を利用して、テキストファイルの保存と読み込み機能（StorageLoadTextController/StorageSaveTextController）を追加。
・ギャラリーアプリを起動して、画像ファイルのパスを取得する機能（GalleryPickController）を追加（サンプルとしてテクスチャへのロードとスクリーンショットを追加）。
・ファイルパスをMediaScannerに登録（更新）する機能（MediaScannerController）を追加。
(ver.1.4)
・バイブレーターの機能を追加（VibratorController）。
・通知（NotificationController）にもバイブレーター機能を追加。
・全ての拡張エディタスクリプトを「SerializedProperty」に置き換え（エディタ上で設定が保存されないことがあったので）。
(ver.1.5)
・バッテリーの温度、コンディション(オーバーヒート、良好、等)、残量、接続状態のステータス取得（リスニング）を追加（BatteryStatusController）。
(ver.1.6)
・画面回転の変化イベント取得（OrientationStatusController）を追加。
・センサーの値の取得機能（～SensorController）を追加。
(ver.1.7)
・各システム設定画面を開くプレファブとデモを追加。
・AndroidActionControllerに「ActionType.ActionOnly」定数(enum)とアクションの入力支援機能を追加。
・MailerController, DialerController, ApplicationDetailsSettingsController 等、専用のアクションコントローラをいくつか追加。
(ver.1.8)
・デバイス認証（指紋・パターン・PIN・パスワード等。ユーザーの設定による）の利用機能を追加。
・実行中デバイスの API Level（int型）の取得機能を追加。
(ver.1.9)
・パーミッション付与のチェック（AndroidPlugin.CheckPermission(), ～Controller.IsPermissionGranted）機能を追加。
・いくつかの「～Controller」[*] に、起動時（Start()）にサポートのチェック（IsSupported～）とパーミッションの付与チェック（IsPermissionGranted）を追加。不可のとき「OnError」コールバックにエラーメッセージを返すようにした。
[*]SpeechRecognizerDialogController, BluetoothSettingController, SpeechRecognizerController, VibratorController, HeartRateController, その他全てのセンサー（IsSupportedSensorのみ）
(ver.1.10)
・QRコード(バーコード)スキャナを起動しテキストを取得する（ShowQRCodeScanner()）機能を追加。
・センサー値の一般的な定数（SensorConstant）を追加。
(ver.1.11)
・StartAction(), StartActionWithChooser(), StartActionURI() に複数パラメタオーバーロードを追加。AndroidActionController も複数パラメタ対応。
・MailerController を複数パラメタアクションに変更（より多くのメーラーが対応できるため）。
・マーケット（Google Play）検索機能を追加（MarketSearchController）。
・アプリのインストールチェック（IsExistApplication()）、アプリ名（GetApplicationName()）、アプリバージョン番号（GetVersionCode()）、アプリバージョン名（GetVersionName()）の取得機能を追加。


※最新版はブログにて GoogleDrive からダウンロードできます（日本語版のみ）。
http://fantom1x.blog130.fc2.com/blog-entry-273.html

------------------------------------------------
■使用ライブラリのライセンス等

このプラグインには Apache License, Version 2.0 のライセンスで配布されている成果物を含んでいます。
http://www.apache.org/licenses/LICENSE-2.0

ZXing ("Zebra Crossing") open source project (google). [ver.3.3.2] (QR Code Scan)
https://github.com/zxing/zxing

------------------------------------------------
■News!

アセットストアにてサンプルの楽曲を含む音楽ライブラリが公開中！

Seamless Loop and Short Music (FREE!)
https://www.assetstore.unity3d.com/#!/content/107732

------------------------------------------------
By Fantom

[Blog] http://fantom1x.blog130.fc2.com/
[Twitter] https://twitter.com/fantom_1x
[SoundCloud] https://soundcloud.com/user-751508071
[Picotune] http://picotune.me/?@Fantom
[Monappy] https://monappy.jp/u/Fantom
[E-Mail] fantom_1x@yahoo.co.jp

