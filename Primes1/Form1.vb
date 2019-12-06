Imports System.Text

Public Class Form1
    Dim thread1 As New System.Threading.Thread(AddressOf calcPrime)
    Dim primes(-1) As ULong
    Dim display As New StringBuilder("2, 3, 5, 7")
    Dim x As ULong
    Const size As ULong = 1000000

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If thread1.IsAlive = True Then
            thread1.Abort()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If thread1.ThreadState <> Threading.ThreadState.Running Then
            If thread1.ThreadState = Threading.ThreadState.Unstarted Then
                thread1.Start()
            End If
            If thread1.ThreadState = Threading.ThreadState.Stopped Then
                Button1.Text = "Restart"
            End If
        End If
        Timer1.Enabled = True
    End Sub

    Sub calcPrime()
        For x = 7 To size
            If (x Mod 2) <> 0 Then
                If (x Mod 3) <> 0 Then
                    If (x Mod 5) <> 0 Then
                        If (x Mod 7) <> 0 Then
                            ReDim Preserve primes(primes.Length)
                            primes(primes.Length - 1) = x
                        End If
                    End If
                End If
            End If
        Next

        'This takes fucking forever FIX FIX FIX
        For i = 0 To primes.Length - 1
            display.Append(", " & primes(i))
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'Label progress
        If x >= size Then
            If thread1.ThreadState = Threading.ThreadState.Stopped Then
                Label1.Text = "Done!"
            Else
                Label1.Text = ""
            End If
        Else
            Label1.Text = x
        End If

        'Progress bar
        ProgressBar1.Value = (x / size) * 100 - 1
        ProgressBar1.PerformStep()

        'Finished
        If thread1.ThreadState = Threading.ThreadState.Stopped Then
            TextBox1.Text = display.ToString
            Timer1.Enabled = False
            Label2.Text = "Ratio:" & primes.Length / size
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Computer.Clipboard.SetText(display.ToString)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If SaveFileDialog1.FileName <> "" Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, display.ToString(), False)
            End If
        End If
    End Sub
End Class
