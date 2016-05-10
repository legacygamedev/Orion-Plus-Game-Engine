﻿Imports System.Windows.Forms
Imports SFML.Audio

Module ClientSound
    'Music + Sound Players
    Public SoundPlayer As Sound
    Public ExtraSoundPlayer As Sound
    Public MusicPlayer As Music
    Public MusicCache(0 To 100) As String
    Public SoundCache(0 To 100) As String

    Public FadeInSwitch As Boolean
    Public FadeOutSwitch As Boolean
    Public CurMusic As String
    Public MaxVolume As Single

    Sub PlayMusic(ByVal FileName As String)
        If Not Options.Music = 1 Or Not FileExist(Application.StartupPath & MUSIC_PATH & FileName) Then Exit Sub
        If FileName = CurMusic Then Exit Sub

        If MusicPlayer Is Nothing Then
            Try
                MusicPlayer = New Music(Application.StartupPath & MUSIC_PATH & FileName)
                MusicPlayer.Loop() = True
                MusicPlayer.Volume() = 0
                MusicPlayer.Play()
                CurMusic = FileName
                FadeInSwitch = True
            Catch ex As Exception

            End Try
        Else
            Try
                FadeOutSwitch = True

            Catch ex As Exception

            End Try
        End If
    End Sub

    Sub StopMusic()
        If MusicPlayer Is Nothing Then Exit Sub
        MusicPlayer.Stop()
        MusicPlayer.Dispose()
        MusicPlayer = Nothing
        CurMusic = ""
    End Sub

    Sub PlaySound(ByVal FileName As String, Optional Looped As Boolean = False)
        If Not Options.Sound = 1 Or Not FileExist(Application.StartupPath & SOUND_PATH & FileName) Then Exit Sub

        Dim buffer As SoundBuffer
        If SoundPlayer Is Nothing Then
            SoundPlayer = New Sound()
            buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
            SoundPlayer.SoundBuffer = buffer
            If Looped = True Then
                SoundPlayer.Loop() = True
            Else
                SoundPlayer.Loop() = False
            End If
            SoundPlayer.Volume() = MaxVolume
            SoundPlayer.Play()
        Else
            SoundPlayer.Stop()
            buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
            SoundPlayer.SoundBuffer = buffer
            If Looped = True Then
                SoundPlayer.Loop() = True
            Else
                SoundPlayer.Loop() = False
            End If
            SoundPlayer.Volume() = MaxVolume
            SoundPlayer.Play()
        End If
    End Sub

    Sub StopSound()
        If SoundPlayer Is Nothing Then Exit Sub
        SoundPlayer.Dispose()
        SoundPlayer = Nothing
    End Sub

    Sub PlayExtraSound(ByVal FileName As String, Optional Looped As Boolean = False)
        If Not Options.Sound = 1 Or Not FileExist(Application.StartupPath & SOUND_PATH & FileName) Then Exit Sub
        'If FileName = CurExtraSound Then Exit Sub

        Dim buffer As SoundBuffer
        If ExtraSoundPlayer Is Nothing Then
            ExtraSoundPlayer = New Sound()
            buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
            ExtraSoundPlayer.SoundBuffer = buffer
            If Looped = True Then
                ExtraSoundPlayer.Loop() = True
            Else
                ExtraSoundPlayer.Loop() = False
            End If
            ExtraSoundPlayer.Volume() = MaxVolume
            ExtraSoundPlayer.Play()
        Else
            ExtraSoundPlayer.Stop()
            buffer = New SoundBuffer(Application.StartupPath & SOUND_PATH & FileName)
            ExtraSoundPlayer.SoundBuffer = buffer
            If Looped = True Then
                ExtraSoundPlayer.Loop() = True
            Else
                ExtraSoundPlayer.Loop() = False
            End If
            ExtraSoundPlayer.Volume() = MaxVolume
            ExtraSoundPlayer.Play()
        End If
    End Sub

    Sub StopExtraSound()
        If ExtraSoundPlayer Is Nothing Then Exit Sub
        ExtraSoundPlayer.Dispose()
        ExtraSoundPlayer = Nothing
    End Sub

    Sub FadeIn()

        If MusicPlayer Is Nothing Then Exit Sub

        If MusicPlayer.Volume() >= MaxVolume Then FadeInSwitch = False
        MusicPlayer.Volume() = MusicPlayer.Volume() + 3

    End Sub

    Sub FadeOut()

        If MusicPlayer Is Nothing Then Exit Sub

        If MusicPlayer.Volume() = 0 Or MusicPlayer.Volume() < 3 Then
            FadeOutSwitch = False
            If Map.Music = Nothing Then
                StopMusic()
            Else
                StopMusic()
                PlayMusic(Map.Music)
            End If
        End If
        MusicPlayer.Volume() = MusicPlayer.Volume() - 3

    End Sub
End Module
