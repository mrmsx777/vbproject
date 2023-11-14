Imports System.Text.RegularExpressions


Public Class Form1
    'Variables Globales:
    Dim numero As String
    Dim initials As String
    Dim birth As String
    Dim sex_index As String
    Dim sex As String



    Dim pattern As String = "^([A-Z]{4})([0-9]{2})(0[1-9]|1[0-2]|5[1-9]|6[0-2])(0[1-9]|[12][0-9]|3[01])(\d{2})$"
    '^ : Commence par
    'Regex Explications: premiere 4 lettres : [A-Z]{4} : 4 valeurs de A à Z 
    'Année valide : [0-9]{2} : 2 Valeurs de 0 à 9
    'Mois 00 -12 ou 51 à 62 : (0[1-9]|1[0-2]|5[1-9]|6[0-2])  : "le nombre 0 et un nombre entre 0 à 9" OU "1 et une valeur de 0 à 2" OU 5 et De 1 à 9 OU 6 et 0 à 2
    'Jours : (0[1-9]|[12][0-9]|3[01]) : 0 et 1 à 9 OU 10 à 29 OU 30,31 
    'Nombre de Distinction : (\d{2}) Deux Nombres (Digits)
    '$ : Termine par


    'A l'ouverture Lire la derniere ligne enregistrée'
    'Lire la derniere Line'
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim lastLine As String = ""
        'Tant que la lecture n'est pas arrivé a la fin Lastline = Line , A la Fin : lastline = Derniere Ligne
        Using file_read As New System.IO.StreamReader("C:\Users\EMICA\Desktop\log.txt")
            Do While Not file_read.EndOfStream
                lastLine = file_read.ReadLine()
            Loop
        End Using
        last_numero.Text = lastLine
    End Sub

    'Enregistrement de la valeur texte sur la variable Numero
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        numero = TextBox1.Text

    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Verification si le Numero est valide par rapport au modéle Regex
        If Regex.IsMatch(numero, pattern) Then
            GoTo Line1

            'Numero Invalide Quiter la section
        Else
            MessageBox.Show("Numero Invalide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


Line1:

        'Recuperation des valeurs du numero
        initials = numero(0) & numero(3) & "    - "
        birth = numero(4) & numero(5) & "/" & numero(6) & numero(7) & "/" & numero(8) & numero(9) & "    - "
        sex_index = numero(6)

        'Identification du sex : valeurs 51 à 62 pour le sex Femme et recuperation de la date normale 01 à 12
        If sex_index = "5" Then
            sex = "Femme"
            birth = numero(4) & numero(5) & "/" & "0" & numero(7) & "/" & numero(8) & numero(9) & "    - "
        ElseIf sex_index = "6" Then
            sex = "Femme"
            birth = numero(4) & numero(5) & "/" & "1" & numero(7) & "/" & numero(8) & numero(9) & "    - "
        Else
            sex = "Homme"
        End If


        'Ecriture Finale du resultat avec les variables correctes:
        resultat.Text = "Initials:   " & initials & "  Naissance:    " & birth & "  Sex:    " & sex
        last_numero.Text = numero

    End Sub

    'Enregistrement du dernier numero saisie sur le Ficher Log avant la Fermeture du Programme'
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim file_write As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("C:\Users\EMICA\Desktop\log.txt", True)
        file_write.WriteLine(numero)
        file_write.Close()
    End Sub


End Class


