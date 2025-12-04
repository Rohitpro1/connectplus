using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConnectPlus.Services
{
    public class AiService
    {
        private Process _whisperProcess;
        private HttpClient _httpClient;
        private bool _isListening = false;

        public AiService()
        {
            _httpClient = new HttpClient();
        }

        public void StartListening()
        {
            // TODO: Implement Whisper real-time transcription
            // This is a placeholder for future implementation
            _isListening = true;
        }

        public void StopListening()
        {
            // TODO: Stop Whisper process
            _isListening = false;

            if (_whisperProcess != null && !_whisperProcess.HasExited)
            {
                try
                {
                    _whisperProcess.Kill();
                    _whisperProcess.WaitForExit(5000);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error stopping Whisper: {ex.Message}");
                }
                finally
                {
                    _whisperProcess?.Dispose();
                    _whisperProcess = null;
                }
            }
        }

        public bool IsListening => _isListening;

        public async Task<string> TranscribeChunk(string audioFilePath)
        {
            // TODO: Call whisper.exe to transcribe audio chunk
            // Example command: whisper.exe --model medium.en.bin --input audio.wav
            if (string.IsNullOrEmpty(AppConfig.WhisperPath) || !File.Exists(AppConfig.WhisperPath))
            {
                return "Whisper not configured";
            }

            // Placeholder implementation
            return await Task.FromResult("Transcription placeholder");
        }

        public async Task<string> GenerateSummary(string text)
        {
            // TODO: Call Ollama API to generate summary
            // POST http://localhost:11434/api/generate
            try
            {
                var requestBody = new
                {
                    model = "llama2",
                    prompt = $"Summarize the following text: {text}",
                    stream = false
                };

                string json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(
                    $"{AppConfig.OllamaUrl}/api/generate",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result?.response?.ToString() ?? "No response from Ollama";
                }
                else
                {
                    return $"Ollama API error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error calling Ollama: {ex.Message}";
            }
        }

        public void Dispose()
        {
            StopListening();
            _httpClient?.Dispose();
        }
    }
}

