window.renderMoodChart = (labels, values, isDarkMode) => {
    const ctx = document.getElementById('moodChart');

    if (!ctx) return;

    // Destroy existing chart if it exists
    if (window.moodChartInstance) {
        window.moodChartInstance.destroy();
    }

    window.moodChartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Mood Count',
                data: values,
                backgroundColor: isDarkMode
                    ? 'rgba(99, 102, 241, 0.8)'
                    : 'rgba(79, 70, 229, 0.8)',
                borderRadius: 8
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        color: isDarkMode ? '#D1D5DB' : '#4B5563'
                    },
                    grid: {
                        color: isDarkMode ? '#374151' : '#E5E7EB'
                    }
                },
                x: {
                    ticks: {
                        color: isDarkMode ? '#D1D5DB' : '#4B5563'
                    },
                    grid: {
                        display: false
                    }
                }
            },
            plugins: {
                legend: { display: false }
            }
        }
    });
};
window.renderMoodPieChart = (labels, values, isDarkMode) => {
    const ctx = document.getElementById('moodPieChart');
    if (!ctx) return;

    if (window.moodPieChartInstance) {
        window.moodPieChartInstance.destroy();
    }

    window.moodPieChartInstance = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: values,
                backgroundColor: [
                    '#6366F1', '#22C55E', '#F59E0B',
                    '#EF4444', '#06B6D4', '#A855F7',
                    '#84CC16', '#F97316'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        color: isDarkMode ? '#E5E7EB' : '#374151'
                    }
                }
            }
        }
    });
};
window.renderTagChart = (labels, values, isDarkMode) => {
    const ctx = document.getElementById('tagChart');

    if (!ctx) return;

    // Destroy existing chart if it exists
    if (window.tagChartInstance) {
        window.tagChartInstance.destroy();
    }

    window.tagChartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Tag Count',
                data: values,
                backgroundColor: isDarkMode
                    ? 'rgba(99, 102, 241, 0.8)'
                    : 'rgba(79, 70, 229, 0.8)',
                borderRadius: 8
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        color: isDarkMode ? '#D1D5DB' : '#4B5563'
                    },
                    grid: {
                        color: isDarkMode ? '#374151' : '#E5E7EB'
                    }
                },
                x: {
                    ticks: {
                        color: isDarkMode ? '#D1D5DB' : '#4B5563'
                    },
                    grid: {
                        display: false
                    }
                }
            },
            plugins: {
                legend: { display: false }
            }
        }
    });
};
window.renderTagPieChart = (labels, values, isDarkMode) => {
    const ctx = document.getElementById('tagPieChart');
    if (!ctx) return;

    if (window.tagPieChartInstance) {
        window.tagPieChartInstance.destroy();
    }

    window.tagPieChartInstance = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: values,
                backgroundColor: [
                    '#6366F1', '#22C55E', '#F59E0B',
                    '#EF4444', '#06B6D4', '#A855F7',
                    '#84CC16', '#F97316'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        color: isDarkMode ? '#E5E7EB' : '#374151'
                    }
                }
            }
        }
    });
};