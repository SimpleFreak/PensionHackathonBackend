using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PensionHackathonBackend.Core.Models
{
    /* Класс файла CSV для дальнейшего представления сущности для базы данных */
    public class FileCSV
    {
        [Key, Required, NotNull]
        public Guid Id { get; set; }

        [Required, NotNull]
        public string FileName { get; set; } = string.Empty;

        [Required, NotNull]
        public string FilePath { get; set; } = string.Empty;

        [Required, DataType(DataType.DateTime), NotNull]
        public DateTime DateAdded { get; set; }

        /* Закрытый конструктор для обеспечения инкапсуляции */
        private FileCSV(Guid id, string fileName, string filePath, DateTime dateAdded)
        {
            Id = id;
            FileName = fileName;
            FilePath = filePath;
            DateAdded = dateAdded;
        }

        /* Реализация паттерна 'Фабричный метод' в виде статического метода
         * по созданию объекта и возрата ошибки при наличии таковой
         */
        public static (FileCSV fileCSV, string Error) Create(Guid id, string fileName,
            string filePath, DateTime dateAdded)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(fileName))
            {
                error = "FileName cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(filePath))
            {
                error += "\nFilePath cannot be undefined or empty.";
            }

            if (string.IsNullOrEmpty(Convert.ToString(dateAdded)))
            {
                error += "\nDateAdded cannot be undefined or empty.";
            }

            var fileCSV = new FileCSV(id, fileName, filePath, dateAdded);

            return (fileCSV, error);
        }
    }
}
