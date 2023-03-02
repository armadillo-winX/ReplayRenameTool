using System;
using System.Windows;
using System.Windows.Controls;

namespace ReplayRenameTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string? _appName = VersionInfo.AppName;
        public MainWindow()
        {
            InitializeComponent();

            this.Title = _appName;

            EnableDragDrop(this);
        }

        private void RenameReplayFile(string replayFile)
        {
            if (TeamComboBox.SelectedIndex >= 0 &&
                PlayerComboBox.SelectedIndex >= 0 &&
                BingoComboBox.SelectedIndex >= 0 &&
                OutputDirectoryBox.Text.Length > 0)
            {
                ComboBoxItem teamItem = (ComboBoxItem)TeamComboBox.SelectedItem;
                string? team = teamItem.Content.ToString();
                ComboBoxItem playerItem = (ComboBoxItem)PlayerComboBox.SelectedItem;
                string? player = playerItem.Content.ToString();
                ComboBoxItem bingoItem = (ComboBoxItem)BingoComboBox.SelectedItem;
                string? bingo = bingoItem.Content.ToString();
                string outputDirPath = OutputDirectoryBox.Text;

                try
                {
                    Replay.Rename(replayFile, team, player, bingo, outputDirPath);
                    MessageBox.Show(this, "リネームしました。", "完了",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "エラー",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (OutputDirectoryBox.Text.Length == 0)
            {
                MessageBox.Show(this, "出力先ディレクトリを指定してください。", _appName,
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show(this, "チーム、選手番号、ビンゴ番号を指定してください。", _appName,
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void EnableDragDrop(Control control)
        {
            //ドラッグ＆ドロップを受け付けられるようにする
            control.AllowDrop = true;

            //ドラッグが開始された時のイベント処理（マウスカーソルをドラッグ中のアイコンに変更）
            control.PreviewDragOver += (s, e) =>
            {
                //ファイルがドラッグされたとき、カーソルをドラッグ中のアイコンに変更し、そうでない場合は何もしない。
                e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : e.Effects = DragDropEffects.None;
                e.Handled = true;
            };

            //ドラッグ＆ドロップが完了した時の処理（ファイル名を取得し、ファイルの中身をTextプロパティに代入）
            control.PreviewDrop += (s, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop)) // ドロップされたものがファイルかどうか確認する。
                {
                    string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                    //--------------------------------------------------------------------
                    // ここに、ドラッグ＆ドロップ受付時の処理を記述する
                    //--------------------------------------------------------------------
                    RenameReplayFile(paths[0]);
                }
            };

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OutputDirectoryBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
