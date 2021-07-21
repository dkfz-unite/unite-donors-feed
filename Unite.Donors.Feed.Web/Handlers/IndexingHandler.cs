﻿using System.Linq;
using Microsoft.Extensions.Logging;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Donors.Feed.Web.Services;
using Unite.Indices.Entities.Donors;
using Unite.Indices.Services;

namespace Unite.Donors.Feed.Web.Handlers
{
    public class IndexingHandler
    {
        private readonly TaskProcessingService _taskProcessingService;
        private readonly IIndexCreationService<DonorIndex> _indexCreationService;
        private readonly IIndexingService<DonorIndex> _indexingService;
        private readonly ILogger _logger;


        public IndexingHandler(
            TaskProcessingService taskProcessingService,
            IIndexCreationService<DonorIndex> indexCreationService,
            IIndexingService<DonorIndex> indexingService,
            ILogger<IndexingHandler> logger)
        {
            _taskProcessingService = taskProcessingService;
            _indexCreationService = indexCreationService;
            _indexingService = indexingService;
            _logger = logger;
        }


        public void Handle(int bucketSize)
        {
            ProcessDonorIndexingTasks(bucketSize);
        }


        private void ProcessDonorIndexingTasks(int bucketSize)
        {
            _taskProcessingService.Process(TaskType.Indexing, TaskTargetType.Donor, bucketSize, (tasks) =>
            {
                _logger.LogInformation($"Indexing {tasks.Length} donors");

                var indices = tasks.Select(task =>
                {
                    var id = int.Parse(task.Target);

                    var index = _indexCreationService.CreateIndex(id);

                    return index;

                }).ToArray();

                _indexingService.IndexMany(indices);

                _logger.LogInformation($"Indexing of {tasks.Length} donors completed");
            });
        }
    }
}