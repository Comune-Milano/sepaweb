
Partial Class CENSIMENTO_FotoImmobile
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then


            Try
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader

                'T=U  Unita immobiliare
                'T=E  Edificio
                'T=C  Complesso
                Dim TipoStabile As String = Request.QueryString("T")


                'ID è l'indice del complesso o dello stabile o dell'unita
                'sempre riferito a T
                Dim TIPO As String = ""

                IdTipoStabile = Request.QueryString("ID")
                Select Case TipoStabile
                    Case "C"
                        par.cmd.CommandText = "select complessi_immobiliari.cod_complesso AS CODICE from siscom_mi.complessi_immobiliari where id=" & IdTipoStabile
                        TIPO = "CO"
                    Case "E"
                        par.cmd.CommandText = "select edifici.cod_edificio AS CODICE from siscom_mi.edifici where id=" & IdTipoStabile
                        TIPO = "ED"
                    Case "U"
                        par.cmd.CommandText = "select unita_immobiliari.cod_unita_immobiliare as CODICE from siscom_mi.UNITA_IMMOBILIARI where id=" & IdTipoStabile
                        TIPO = "UI"
                End Select
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    COD_TIPO_STABILE = par.IfNull(myreader("CODICE"), "")
                End If
                myreader.Close()

                par.cmd.CommandText = "SELECT INDIRIZZI.*,COMUNI_NAZIONI.NOME AS COMUNE FROM COMUNI_NAZIONI,SISCOM_MI.INDIRIZZI WHERE COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID=" & Request.QueryString("I")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    Image1.Attributes.Add("onclick", "javascript:window.open('http://www.google.it/maps?q=" & par.VaroleDaPassare(myreader("DESCRIZIONE") & " " & myreader("civico") & " " & myreader("comune") & " " & myreader("cap")) & "','','');")
                End If
                myreader.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                ElencoFoto(TIPO)
                ElencoPlanimetrie(TIPO)

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
    End Sub

    'Public Property FotoEdificio() As String
    '    Get
    '        If Not (ViewState("par_FotoEdificio") Is Nothing) Then
    '            Return CStr(ViewState("par_FotoEdificio"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_FotoEdificio") = value
    '    End Set
    'End Property


    Public Property IdTipoStabile() As String
        Get
            If Not (ViewState("par_IdTipoStabile") Is Nothing) Then
                Return CStr(ViewState("par_IdTipoStabile"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdTipoStabile") = value
        End Set
    End Property




    'Public Property IdUnitaImmob() As String
    '    Get
    '        If Not (ViewState("par_IdUnitaImmob") Is Nothing) Then
    '            Return CStr(ViewState("par_IdUnitaImmob"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_IdUnitaImmob") = value
    '    End Set
    'End Property

    Public Property PlanimetrieTipoStabile() As String
        Get
            If Not (ViewState("par_PlanimetrieTipoStabile") Is Nothing) Then
                Return CStr(ViewState("par_PlanimetrieTipoStabile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_PlanimetrieTipoStabile") = value
        End Set
    End Property

    Public Property FotoTipoStabile() As String
        Get
            If Not (ViewState("par_FotoTipoStabile") Is Nothing) Then
                Return CStr(ViewState("par_FotoTipoStabile"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_FotoTipoStabile") = value
        End Set
    End Property

    Public Sub TabFoto()
        If FotoTipoStabile <> "" Then
            Response.Write("<div class=" & Chr(34) & "tabbertab" & Chr(34) & " style=" & Chr(34) & "BACKGROUND-COLOR: white;width:100%;height:300px;overflow-x: scroll;overflow-y: scroll" & Chr(34) & ">")
            Response.Write("<h2>Foto</h2>")
            Response.Write(FotoTipoStabile)
            Response.Write("</div>")
        End If
    End Sub

    Public Sub TabPlanimetrie()
        If PlanimetrieTipoStabile <> "" Then
            Response.Write("<div class=" & Chr(34) & "tabbertab" & Chr(34) & " style=" & Chr(34) & "BACKGROUND-COLOR: white;width:100%;height:300px;overflow-x: scroll;overflow-y: scroll" & Chr(34) & ">")
            Response.Write("<h2>Planimetrie</h2>")
            Response.Write(PlanimetrieTipoStabile)
            Response.Write("</div>")
        End If
    End Sub

    Private Function ElencoPlanimetrie(ByVal tipo As String)
        Dim MioColore As String
        Dim ElencoFile() as string

        PlanimetrieTipoStabile = "<table border='0' cellpadding='1' cellspacing='1' style='BACKGROUND-COLOR: white;width:90%'>"
        PlanimetrieTipoStabile = PlanimetrieTipoStabile & "<tr>" & vbCrLf
        PlanimetrieTipoStabile = PlanimetrieTipoStabile & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:50%'><font face='Arial' size='1'>DESCRIZIONE</font></td>" & vbCrLf
        PlanimetrieTipoStabile = PlanimetrieTipoStabile & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:50%'><font face='Arial' size='1'>PLANIMETRIA</font></td>" & vbCrLf
        Dim I As Integer = 0




        MioColore = "#e3e1e1"
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/PLANIMETRIE/" & tipo & "/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & COD_TIPO_STABILE & "*.*")
            ReDim Preserve ElencoFile(I)
            ElencoFile(I) = foundFile
            I = I + 1
        Next

        Dim kk As Long
        Dim jj As Long
        Dim scambia As String = ""

        For kk = 0 To I - 2
            For jj = kk + 1 To I - 1
                '                If CLng(Mid(RicavaFile(ElencoFile(kk)), 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), 1, 14)) Then
                If RicavaFile(ElencoFile(kk)) < RicavaFile(ElencoFile(jj)) Then
                    scambia = ElencoFile(kk)
                    ElencoFile(kk) = ElencoFile(jj)
                    ElencoFile(jj) = scambia
                End If
            Next
        Next
        Dim LL As Integer = 1

        Dim J As Integer = 0
        If I > 0 Then
            For J = 0 To I - 1

                PlanimetrieTipoStabile = PlanimetrieTipoStabile & "<tr>" & vbCrLf
                PlanimetrieTipoStabile = PlanimetrieTipoStabile & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:50%'><font face='Arial' size='1'>Planimetria " & LL & " (" & RicavaFile(ElencoFile(J)) & ")</font></td>"
                PlanimetrieTipoStabile = PlanimetrieTipoStabile & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:50%'><p align='center'><font face='Arial' size='1'><a href='FILE/PLANIMETRIE/" & tipo & "/" & RicavaFile(ElencoFile(J)) & "' target='_blank'><img border='0' src='../immagini/VisFoto.gif'>Visualizza</a></font></td>"
                PlanimetrieTipoStabile = PlanimetrieTipoStabile & "</tr>" & vbCrLf
                If MioColore = "#e3e1e1" Then
                    MioColore = "#cccacb"
                Else
                    MioColore = "#e3e1e1"
                End If
                LL = LL + 1
                'If j = 10 Then Exit For
            Next J
        End If
        PlanimetrieTipoStabile = PlanimetrieTipoStabile & "</table>"
    End Function



    Private Function ElencoFoto(ByVal TIPO As String)
        Dim MioColore As String
        Dim ElencoFile() as string

        FotoTipoStabile = "<table border='0' cellpadding='1' cellspacing='1' style='BACKGROUND-COLOR: white;width:90%'>"
        FotoTipoStabile = FotoTipoStabile & "<tr>" & vbCrLf
        FotoTipoStabile = FotoTipoStabile & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:50%'><font face='Arial' size='1'>DESCRIZIONE</font></td>" & vbCrLf
        FotoTipoStabile = FotoTipoStabile & "<td bgcolor=" & Chr(34) & "darkgray" & Chr(34) & " style='width:50%'><font face='Arial' size='1'>FOTO</font></td>" & vbCrLf
        Dim I As Integer = 0




        MioColore = "#e3e1e1"
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("FILE/FOTO/" & TIPO & "/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & COD_TIPO_STABILE & "*.*")
            ReDim Preserve ElencoFile(I)
            ElencoFile(I) = foundFile
            I = I + 1
        Next

        Dim kk As Long
        Dim jj As Long
        Dim scambia As String = ""

        For kk = 0 To I - 2
            For jj = kk + 1 To I - 1
                '                If CLng(Mid(RicavaFile(ElencoFile(kk)), 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), 1, 14)) Then
                If RicavaFile(ElencoFile(kk)) < RicavaFile(ElencoFile(jj)) Then
                    scambia = ElencoFile(kk)
                    ElencoFile(kk) = ElencoFile(jj)
                    ElencoFile(jj) = scambia
                End If
            Next
        Next
        Dim LL As Integer = 1

        Dim J As Integer = 0
        If I > 0 Then
            For j = 0 To I - 1

                FotoTipoStabile = FotoTipoStabile & "<tr>" & vbCrLf
                FotoTipoStabile = FotoTipoStabile & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:50%'><font face='Arial' size='1'>Foto " & LL & " (" & RicavaFile(ElencoFile(J)) & ")</font></td>"
                FotoTipoStabile = FotoTipoStabile & "<td bgcolor=" & Chr(34) & MioColore & Chr(34) & " style='width:50%'><p align='center'><font face='Arial' size='1'><a href='FILE/FOTO/" & TIPO & "/" & RicavaFile(ElencoFile(J)) & "' target='_blank'><img border='0' src='../immagini/VisFoto.gif'>Visualizza</a></font></td>"
                FotoTipoStabile = FotoTipoStabile & "</tr>" & vbCrLf
                If MioColore = "#e3e1e1" Then
                    MioColore = "#cccacb"
                Else
                    MioColore = "#e3e1e1"
                End If
                LL = LL + 1

                'If j = 10 Then Exit For
            Next j
        End If
        FotoTipoStabile = FotoTipoStabile & "</table>"
    End Function


   
    'Private Property IdEdificio() As String
    '    Get
    '        If Not (ViewState("par_IdEdificio") Is Nothing) Then
    '            Return CStr(ViewState("par_IdEdificio"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_IdEdificio") = value
    '    End Set

    'End Property

    'Private Property CodComplesso() As String
    '    Get
    '        If Not (ViewState("par_CodComplesso") Is Nothing) Then
    '            Return CStr(ViewState("par_CodComplesso"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_CodComplesso") = value
    '    End Set

    'End Property

    'Private Property IdComplesso() As String
    '    Get
    '        If Not (ViewState("par_IdComplesso") Is Nothing) Then
    '            Return CStr(ViewState("par_IdComplesso"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_IdComplesso") = value
    '    End Set

    'End Property


    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Private Property COD_TIPO_STABILE() As String
        Get
            If Not (ViewState("par_COD_TIPO_STABILE") Is Nothing) Then
                Return CStr(ViewState("par_COD_TIPO_STABILE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_COD_TIPO_STABILE") = value
        End Set
    End Property


    'Private Property COD_Complesso() As String
    '    Get
    '        If Not (ViewState("par_COD_Complesso") Is Nothing) Then
    '            Return CStr(ViewState("par_COD_Complesso"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_COD_Complesso") = value
    '    End Set
    'End Property

    'Private Property COD_Edificio() As String
    '    Get
    '        If Not (ViewState("par_COD_Edificio") Is Nothing) Then
    '            Return CStr(ViewState("par_COD_Edificio"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_COD_Edificio") = value
    '    End Set
    'End Property

    'Private Property COD_UnitaImmobiliare() As String
    '    Get
    '        If Not (ViewState("par_COD_UnitaImmobiliare") Is Nothing) Then
    '            Return CStr(ViewState("par_COD_UnitaImmobiliare"))
    '        Else
    '            Return ""
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_COD_UnitaImmobiliare") = value
    '    End Set
    'End Property

End Class
